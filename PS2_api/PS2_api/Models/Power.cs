using System.ComponentModel.DataAnnotations;
using PS2_api.Base;

namespace PS2_api.Models;

public class Power : Entity
{
    [Required] public float mWh { get; set; }
    
}