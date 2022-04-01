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

    [HttpGet("{hour}")]
    public async Task<List<Waypoint>> GET(int hour)
    {
        var pw = await _dbContext.Waypoints.Where(d => d.positionTime.Hour == hour).ToListAsync();
        return pw;
    }

}