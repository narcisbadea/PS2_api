using Microsoft.AspNetCore.Mvc;
using PS2_api.DataBase;
using PS2_api.Models;

namespace PS2_api.Controllers;


[ApiController]
[Route("api")]
public class PowersController:ControllerBase
{
    private readonly AppDbContext _dbContext;
    
    public PowersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpPost("{wh}")]
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
}