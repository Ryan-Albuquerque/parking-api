using System.ComponentModel.DataAnnotations;

namespace Parking.DTO
{
    public class FinishParking
    {
        [Required(ErrorMessage = "O campo GetOutTime é obrigatório.")]
        public required DateTime GetOutTime { get; set; }

        [Required(ErrorMessage = "O campo GetOutUserId é obrigatório.")]
        public required Guid GetOutUserId { get; set; }

        [Required(ErrorMessage = "O campo ParkId é obrigatório.")]
        public required Guid ParkId { get; set; }
    }
}
