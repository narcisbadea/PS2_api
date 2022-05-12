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
        
        var lastPower = await _dbContext.Powers.ToListAsync();
        var lastTime = lastPower.Last().Created;
        if (lastTime.AddSeconds(30) < pl.Created)
        {
            var prw = new Power
            {
                Id = Guid.NewGuid(),
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
        pw.Id = Guid.NewGuid();
        pw.Created = DateTime.UtcNow;
        TimeSpan ts = new TimeSpan((int)pr.hour, pw.Created.Minute, pw.Created.Second);
        pw.Created = pw.Created.Date + ts;
        pw.mWh = pr.mWh;
        var add = await _dbContext.Powers.AddAsync(pw);
        _dbContext.SaveChanges();
        return Ok(pw);
    }
    [HttpGet("live")]
    public async Task<List<PowerLiveResult>> GetPowerLive()
    {
        var pl = await _dbContext.PowerLives.ToListAsync();
        var pr = new PowerLiveResult();
        pr.Time = pl[0].Created.Hour + ":" + pl[0].Created.Minute + ":" + pl[0].Created.Second;
        pr.livePower = pl[0].livePower;
        pr.totalPower = pl[0].totalPower;

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

            r.hour += ((double)p.Created.Minute * 100 / 60) / 100;

            r.mWh = p.mWh;
            pr.Add(r);
        }

        return pr;
    }
}