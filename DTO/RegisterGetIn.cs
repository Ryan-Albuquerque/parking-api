namespace Parking.DTO
{
    public class RegisterGetIn
    {
        public DateTime GetInTime { get; set; }
        public Guid GetInUserId { get; set; }
        public string LicensePlate { get; set; } = string.Empty;
        public Guid ParkId { get; set; }
    }
}
