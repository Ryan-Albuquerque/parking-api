using System.ComponentModel.DataAnnotations;

namespace Parking.Data.DTO
{
    public class StartParkingDto
    {
        [Required(ErrorMessage = "O campo GetInTime é obrigatório.")]
        public required DateTime GetInTime { get; set; }

        [Required(ErrorMessage = "O campo GetInUserId é obrigatório.")]
        public required string GetInUserId { get; set; }

        [Required(ErrorMessage = "O campo LicensePlate é obrigatório.")]
        public required string LicensePlate { get; set; }

        [Required(ErrorMessage = "O campo ParkId é obrigatório.")]
        public required Guid ParkId { get; set; }
    }
}