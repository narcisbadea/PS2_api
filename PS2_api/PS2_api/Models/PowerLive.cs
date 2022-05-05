using System.ComponentModel.DataAnnotations;

namespace PS2_api.Models;

public class PowerLive
{
    
    [Required] public int Id { get; set; }
    [Required] public float livePower { get; set; }
    [Required] public float totalPower { get; set; }
    [Required] public DateTime Created { get; set; }
    
}