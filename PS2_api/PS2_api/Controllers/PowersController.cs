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
    public async Task<PowerLive> Put(PowerRequest powerRequest)
    {
        var powerLive = await _dbContext.PowerLives.FirstOrDefaultAsync(p => p.Id == 1);
        if (powerLive is null)
        {
            throw new ArgumentException("PowerLive not found!");
        }

        powerLive.livePower = powerRequest.livePower; //= new PowerLive// update power live from database with new data
        powerLive.totalPower = powerRequest.totalPower;
        powerLive.Created = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        
        if (powerLive.Created.Hour == 0 && powerLive.Created.Minute == 0 && powerLive.Created.Second is 0 or 1) // reset at 00:00:00
        {
            foreach (var pow in await _dbContext.Powers.ToListAsync())
            {
                _dbContext.Powers.Remove(pow);
            }
            await _dbContext.SaveChangesAsync();
            
            var firstPowerOfTheDay = new Power
            {
                mWh = powerLive.livePower,
                Created = powerLive.Created
            };
            await _dbContext.Powers.AddAsync(firstPowerOfTheDay);
            await _dbContext.SaveChangesAsync();
        }
        
        var lastPowerRegistered = await _dbContext.Powers.OrderBy(p => p.Created).LastAsync();
        var lastTime = lastPowerRegistered.Created;
       
        if (lastTime.AddMinutes(1) < powerLive.Created)//once a minute add the current live power to the database
        {
            var powerLiveForAdd = new Power
            {
                mWh = powerLive.livePower,
                Created = powerLive.Created
            };
            await _dbContext.Powers.AddAsync(powerLiveForAdd);
        }
        await _dbContext.SaveChangesAsync();
        return powerLive;
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
        var powers = await _dbContext.Powers.OrderBy(p => p.Created).ToListAsync();
        List<PowerResult> powerResults = new List<PowerResult>();
        foreach (var power in powers)
        {
            powerResults.Add(new PowerResult
            {
                hour = power.Created.Hour + ((double)power.Created.Minute * 100 / 60) / 100,
                mWh = power.mWh,
            });
        }
        return powerResults;
    }
}