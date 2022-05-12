using System.ComponentModel.DataAnnotations;

namespace PS2_api.Models;

public class Waypoint
{
    [Required] public int Id { get; set; }
    [Required] public TimeOnly positionTime { get; set; }
    [Required] public int LR { get; set; }
    [Required] public int TD { get; set; }
}