using System.ComponentModel.DataAnnotations;

namespace Parking.Data.DTO
{
    public class FinishParkingDto
    {
        [Required(ErrorMessage = "O campo GetOutTime é obrigatório.")]
        public required DateTime GetOutTime { get; set; }

        [Required(ErrorMessage = "O campo GetOutUserId é obrigatório.")]
        public required string GetOutUserId { get; set; }

        [Required(ErrorMessage = "O campo ParkId é obrigatório.")]
        public required Guid ParkId { get; set; }
    }
}
