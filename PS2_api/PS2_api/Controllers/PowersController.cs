using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PS2_api.DataBase;
using PS2_api.Models;

namespace PS2_api.Controllers;


[ApiController]
[Route("[controller]")]
public class PowersController:ControllerBase
{
    private readonly AppDbContext _dbContext;
    
    public PowersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpPost]
    public async Task<Power> Post(PowerRequest pr)
    {
            var power = new Power()
            {
                Id = new Guid(),
                Wh = pr.Wh,
                Created = DateTime.UtcNow
            };

            var result = await _dbContext.Powers.AddAsync(power);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
    }

    [HttpPut]
    public async Task<PowerLive> Put(PowerRequest pr)
    {
        var pl = await _dbContext.PowerLives.FirstOrDefaultAsync(p => p.Id == 1);
        if (pl is null)
        {
            throw new ArgumentException("PowerLive not found!");
        }
        
        pl.Wh = pr.Wh;
        pl.Time = DateTime.UtcNow;
        var lastPowerRegisterd = _dbContext.Powers.Last();
        if (lastPowerRegisterd.Created.AddMinutes(1) < pl.Time)
        {
            await _dbContext.Powers.AddAsync(new Power { Id = new Guid(), Wh = pr.Wh, Created = pl.Time });
        }
        await _dbContext.SaveChangesAsync();
        return pl;
    }

    [HttpGet("live")]
    public async Task<List<PowerLiveResult>> GetPowerLive()
    {
        var pl = await _dbContext.PowerLives.ToListAsync();
        var pr = new PowerLiveResult();
        pr.Time = pl[0].Time.Hour + ":" + pl[0].Time.Minute + ":" + pl[0].Time.Second;
        pr.Wh = pl[0].Wh;
        
        List<PowerLiveResult> result = new List<PowerLiveResult>();
        result.Add(pr);
        return result;
    }
    
    [HttpGet]
    public async Task<List<PowerResult>> GET()
    {
        var l = await _dbContext.Powers.OrderBy(o => o.Created).ToListAsync();
        List<PowerResult> pr = new List<PowerResult>();
        foreach (var p in l)
        {
            PowerResult r = new PowerResult();
            r.hour = p.Created.Hour;
            if (p.Created.Minute == 30)
            {
                r.hour += 0.5;
            }

            r.Wh = p.Wh;
            pr.Add(r);
        }

        return pr;
    }
}