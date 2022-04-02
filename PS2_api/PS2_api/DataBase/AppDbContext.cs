using Microsoft.EntityFrameworkCore;
using PS2_api.Models;

namespace PS2_api.DataBase;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {}
    public DbSet<Power> Powers { get; set; }
    
    public DbSet<Waypoint> Waypoints { get; set; }

    public DbSet<PowerLive> PowerLives { get; set; }
}