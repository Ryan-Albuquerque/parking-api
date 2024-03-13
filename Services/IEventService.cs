using Parking.DTO;
using Parking.Models;

namespace Parking.Services
{
    public interface IEventService
    {
        Event? RegisterEvent(RegisterGetIn data);
    }
}
