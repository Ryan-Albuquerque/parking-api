using Parking.DTO;
using Parking.Models;
using Parking.Utils;

namespace Parking.Services
{
    public interface IEventService
    {
        ResponseHandler<Event> RegisterEvent(RegisterGetIn data);
    }
}
