using System.ComponentModel.DataAnnotations;

namespace Parking.Models
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid ParkId { get; set; }

        [Required]
        public string GetInTime { get; set; } = string.Empty;

        [Required]
        public string GetOutTime { get; set; } = string.Empty;
        
        [Required]
        public Guid GetInUserId { get; set; }

        public Guid GetOutUserId{ get; set; }

        [Required]
        public float PaidValueInCents { get; set; }

        [Required]
        public string LicensePlate { get; set; } = string.Empty;
    }
}
