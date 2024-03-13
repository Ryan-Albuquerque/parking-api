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
        public DateTime GetInTime { get; set; }

        public DateTime? GetOutTime { get; set; }

        [Required]
        public Guid GetInUserId { get; set; }

        public Guid? GetOutUserId { get; set; }

        [Required]
        public decimal PaidValueInCents { get; set; }

        [Required]
        public string LicensePlate { get; set; } = string.Empty;
    }
}
