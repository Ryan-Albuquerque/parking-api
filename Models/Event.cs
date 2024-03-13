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
        public required string GetInUserId { get; set; }

        public string? GetOutUserId { get; set; }

        [Required]
        public decimal PaidValueInCents { get; set; }

        [Required]
        public string LicensePlate { get; set; } = string.Empty;
    }
}
