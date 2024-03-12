using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class Park
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public float PricePerHourInCents { get; set; }
    }
}
