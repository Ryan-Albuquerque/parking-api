using Parking.DTO;
using Parking.Models;
using Parking.Utils;

namespace Parking.Services
{
    public class EventService(ApplicationDbContext context) : IEventService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly int _CENTS = 100;

        public ResponseHandler<List<Event>> ListEventHistory(int page, int total, Guid parkId)
        {
            var eventsFineshed = _context.Events
                .Where(e => e.ParkId == parkId && e.GetOutTime != null)
                .Skip((page - 1) * total)
                .Take(total)
                .ToList();

            return new ResponseHandler<List<Event>>(eventsFineshed, null);
        }

        public ResponseHandler<Event> StartParkingEvent(StartParking data)
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

        public ResponseHandler<Event> FinishParkingEvent(FinishParking data)
        {
            var eventToFinish = _context.Events.FirstOrDefault(e => e.ParkId == data.ParkId && e.GetOutTime == null);

            if (eventToFinish is not null)
            {
                var park = _context.Parks.FirstOrDefault(e => e.Id == data.ParkId);

                if(park is null)
                {
                    return new ResponseHandler<Event>(null, "Não foi possível encontrar o estacionamento, revise `parkId`");
                }

                eventToFinish.PaidValueInCents = CalculateParkingFee(eventToFinish.GetInTime, data.GetOutTime, park.PricePerHourInCents);
                eventToFinish.GetOutUserId = data.GetOutUserId;
                eventToFinish.GetOutTime = data.GetOutTime;

                _context.Events.Update(eventToFinish);
                _context.SaveChanges();

                return new ResponseHandler<Event>(eventToFinish, null);
            }

            return new ResponseHandler<Event>(null, "Não há estacionamento para fechar");
        }

        private float CalculateParkingFee(DateTime entryTime, DateTime exitTime, float hourlyRate)
        {
            TimeSpan parkingDuration = exitTime - entryTime;
            double totalHours = Math.Ceiling(parkingDuration.TotalHours);

            float totalFeeInCents = Convert.ToSingle(totalHours) * hourlyRate;

            float totalFeeInReal = totalFeeInCents / _CENTS;

            return totalFeeInReal;
        }
    }
}
