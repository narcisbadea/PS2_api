using System.ComponentModel.DataAnnotations;

namespace PS2_api.Base;

public class Entity
{
    [Required] public int Id { get; set; } 
    
    [Required] public DateTime Created { get; set; }
}