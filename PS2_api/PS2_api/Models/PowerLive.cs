using System.ComponentModel.DataAnnotations;
using PS2_api.Base;

namespace PS2_api.Models;

public class PowerLive : Entity
{
    [Required] public float livePower { get; set; }
    [Required] public float totalPower { get; set; }
}