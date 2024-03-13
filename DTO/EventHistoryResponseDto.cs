using Parking.Models;

namespace Parking.DTO
{
    public class EventHistoryResponseDto : Event
    {
        public string? GetInUserName { get; set; }
        public string? GetOutUserName { get; set; }
    }
}
