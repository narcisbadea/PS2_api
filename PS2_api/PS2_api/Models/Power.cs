using System.ComponentModel.DataAnnotations;

namespace PS2_api.Models;

public class Power
{
    [Required] 
    public Guid Id { get; set; }
    
    [Required]
    public int Wh { get; set; }
    
    [Required]
    public DateTime  Created { get; set; }
}