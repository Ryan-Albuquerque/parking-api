using Parking.DTO;
using Parking.Models;
using Parking.Utils;

namespace Parking.Services
{
    public class EventService(ApplicationDbContext context) : IEventService
    {
        private readonly ApplicationDbContext _context = context;

        public ResponseHandler<Event> RegisterEvent(RegisterGetIn data)
        {
            bool isAlreadyParking = _context.Events.Any(e => e.ParkId == data.ParkId
                                                             && e.GetInTime < DateTime.UtcNow
                                                             && e.GetOutTime == null);

            if (!isAlreadyParking)
            {
                Event newEvent = new()
                {
                    Id = Guid.NewGuid(),
                    GetInTime = data.GetInTime,
                    GetInUserId = data.GetInUserId,
                    LicensePlate = data.LicensePlate,
                    ParkId = data.ParkId,
                };

                _context.Events.Add(newEvent);
                _context.SaveChanges(); 

                return new ResponseHandler<Event>(newEvent, null);
            }

            return new ResponseHandler<Event>(null, "Estacionamento em uso");
        }
    }
}
