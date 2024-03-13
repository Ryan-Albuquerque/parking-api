using Parking.DTO;
using Parking.Models;

namespace Parking.Services
{
    public class EventService(ApplicationDbContext context) : IEventService
    {
        private readonly ApplicationDbContext _context = context;

        public Event? RegisterEvent(RegisterGetIn data)
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

                return newEvent;
            }

            return null;
        }
    }
}
