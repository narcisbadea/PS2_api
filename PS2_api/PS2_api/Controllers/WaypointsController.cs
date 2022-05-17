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

    [HttpGet("{hour:int}")]
    public async Task<List<Waypoint>> Get(int hour)
    {
        var pw = await _dbContext.Waypoints.Where(d => d.positionTime.Hour == hour).ToListAsync();
        return pw;
    }

    [HttpGet]
    public async Task<List<WaypointResult>> getWay()
    {
        var pw = await _dbContext.Waypoints.OrderBy(w => w.positionTime).ToListAsync();
        var timeNow = TimeOnly.FromDateTime(DateTime.Now);
        var length = pw.Count;
        List<WaypointResult> waypoints = new List<WaypointResult>();
        for (int i = 0; i < length; i++)
        {
            if (pw[i].positionTime <= timeNow && pw[i + 1].positionTime >= timeNow)
            {
                waypoints.Add(new WaypointResult
                {
                    LR = pw[i].LR,
                    TD = pw[i].TD,
                    positionTime = pw[i].positionTime.Hour.ToString()+":"+pw[i].positionTime.Minute.ToString()+":"+pw[i].positionTime.Minute.ToString()
                });
                waypoints.Add(new WaypointResult
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