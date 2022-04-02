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
    public async Task<Power> Post(int wh)
    {
            var power = new Power()
            {
                Id = new Guid(),
                Wh = wh,
                Created = DateTime.UtcNow
            };

            var result = await _dbContext.Powers.AddAsync(power);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
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