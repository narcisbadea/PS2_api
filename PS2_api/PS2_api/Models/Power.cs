using System.ComponentModel.DataAnnotations;

namespace PS2_api.Models;

public class Power
{
    [Required] 
    public Guid Id { get; set; }
    
    [Required]
    public float Wh { get; set; }
    
    [Required]
    public DateTime  Created { get; set; }

    public Power(PowerLive pl)
    {
        this.Id = Guid.NewGuid();
        this.Wh = pl.Id;
        this.Created = pl.Created;
    }
}