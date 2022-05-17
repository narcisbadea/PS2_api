using System.ComponentModel.DataAnnotations;

namespace PS2_api.Models;

public class WaypointResult
{
    [Required] public string positionTime { get; set; }
    [Required] public int LR { get; set; }
    [Required] public int TD { get; set; }
}