using Parking.DTO;
using Parking.Models;
using Parking.Utils;

namespace Parking.Services
{
    public interface IEventService
    {
        ResponseHandler<Event> StartParkingEvent(StartParking data);
        ResponseHandler<Event> FinishParkingEvent(FinishParking data);
        ResponseHandler<List<Event>> ListEventHistory(int page, int total, Guid parkId);
    }
}
