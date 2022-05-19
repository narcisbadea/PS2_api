using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS2_api.DataBase;
using PS2_api.Models;

namespace PS2_api.Controllers;


[ApiController]
[Route("[controller]")]
public class WaypointsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public WaypointsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<WaypointResult>> getWay()
    {
        var pw = await _dbContext.Waypoints.OrderBy(w => w.positionTime).ToListAsync();
        
        var info = TimeZoneInfo.FindSystemTimeZoneById("E. Europe Standard Time");
        DateTimeOffset localServerTime = DateTimeOffset.Now;
        DateTimeOffset localTime= TimeZoneInfo.ConvertTime(localServerTime, info);
    
        var timeNow = TimeOnly.FromDateTime(localTime.DateTime);
        var length = pw.Count;
        
        
        List<WaypointResult> waypoints = new List<WaypointResult>();
        for (int i = 0; i < length; i++)
        {
            if (pw[i].positionTime <= timeNow && pw[i + 1].positionTime >= timeNow)
            {
                var way1 = new WaypointResult
                {
                    LR = pw[i].LR,
                    TD = pw[i].TD
                };
                waypoints.Add(way1);
                
                waypoints.Add(
                    new WaypointResult
                {
                    LR = pw[i+1].LR,
                    TD = pw[i+1].TD,
                    positionTime = pw[i+1].positionTime.Hour.ToString()+":"+pw[i+1].positionTime.Minute.ToString()+":"+pw[i+1].positionTime.Minute.ToString()
                });
                break;
            }
        }

        return waypoints;
    }

}