using Parking.Data.DTO;
using Parking.Models;
using Parking.Utils;

namespace Parking.Services.Interfaces
{
    public interface IEventService
    {
        ResponseHandler<Event> StartParkingEvent(StartParkingDto data);
        ResponseHandler<Event> FinishParkingEvent(FinishParkingDto data);
        ResponseHandler<List<EventHistoryResponseDto>> ListEventHistory(int page, int total, Guid parkId);
    }
}
