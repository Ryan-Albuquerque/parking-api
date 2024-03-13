using Parking.Models;

namespace Parking.Data.DTO
{
    public class EventHistoryResponseDto : Event
    {
        public string? GetInUserName { get; set; }
        public string? GetOutUserName { get; set; }
    }
}
