namespace Parking.Data.DTO
{
    public class EventHistoryDto
    {
        public required List<EventResponseDto> Events { get; set; }
        public int TotalSize { get; set; }
    }
}
