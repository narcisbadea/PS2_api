using System.ComponentModel.DataAnnotations;

namespace PS2_api.Models;

public class PowerLive
{
    
    [Required] public int Id { get; set; }
    [Required] public int Wh { get; set; }
    [Required] public DateTime Time { get; set; }
    
}