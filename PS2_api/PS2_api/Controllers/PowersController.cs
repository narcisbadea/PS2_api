using System.Security.Cryptography;
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
    
    [HttpPut("live")]
    public async Task<PowerLive> Put(PowerRequest pr)
    {
        var pl = await _dbContext.PowerLives.FirstOrDefaultAsync(p => p.Id == 1);
        if (pl is null)
        {
            throw new ArgumentException("PowerLive not found!");
        }
        
        pl.livePower = pr.livePower;
        pl.Created = DateTime.UtcNow;
        pl.totalPower = pr.totalPower;
        
        if (pl.Created.Hour == 0 && pl.Created.Minute == 0 && pl.Created.Second is 0 or 1)
        {
            foreach (var pow in await _dbContext.Powers.ToListAsync())
            {
                _dbContext.Powers.Remove(pow);
            }
            var powAdd = new Power
            {
                mWh = pl.livePower,
                Created = pl.Created
            };
            await _dbContext.Powers.AddAsync(powAdd);
            await _dbContext.SaveChangesAsync();
        }
        
        var lastPower = await _dbContext.Powers.ToListAsync();
        var lastTime = lastPower.Last().Created;
       
        if (lastTime.AddMinutes(1) < pl.Created)
        {
            var prw = new Power
            {
                mWh = pl.livePower,
                Created = pl.Created
            };
            await _dbContext.Powers.AddAsync(prw);
        }
        await _dbContext.SaveChangesAsync();
        return pl;
    }

    [HttpPost]
    public async Task<ActionResult<PowerResult>> post(PowerResult pr)
    {
        Power pw = new Power();
        pw.Created = DateTime.UtcNow;
        TimeSpan ts = new TimeSpan((int)pr.hour, pw.Created.Minute, pw.Created.Second);
        pw.Created = pw.Created.Date + ts;
        pw.mWh = pr.mWh;
        var add = await _dbContext.Powers.AddAsync(pw);
        await _dbContext.SaveChangesAsync();
        return Ok(pw);
    }
    [HttpGet("live")]
    public async Task<List<PowerLiveResult>>GetPowerLive()
    {
        var powerLive = await _dbContext.PowerLives.ToListAsync();
        var powerLiveResult = new PowerLiveResult
        {
            Time = powerLive[0].Created.Hour + ":" + powerLive[0].Created.Minute + ":" + powerLive[0].Created.Second,
            livePower = powerLive[0].livePower,
            totalPower = powerLive[0].totalPower
        };

        return new List<PowerLiveResult> { powerLiveResult };
    }
    
    [HttpGet]
    public async Task<List<PowerResult>> Get()
    {
        var l = await _dbContext.Powers.OrderBy(o => o.Created).ToListAsync();
        List<PowerResult> pr = new List<PowerResult>();
        foreach (var p in l)
        {
            PowerResult r = new PowerResult();
            r.hour = p.Created.Hour;

            r.hour += ((double)p.Created.Minute * 100 / 60) / 100;

            r.mWh = p.mWh;
            pr.Add(r);
        }

        return pr;
    }
}