using System.ComponentModel.DataAnnotations;

namespace PS2_api.Models;

public class Waypoint
{
    [Required] public Guid Id { get; set; }
    [Required] public DateTime positionTime { get; set; }
    [Required] public int LR { get; set; }
    [Required] public int TD { get; set; }
}