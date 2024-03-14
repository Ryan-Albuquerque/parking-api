using Parking.Data.DTO;
using Parking.Models;
using Parking.Services.Interfaces;
using Parking.Utils;

namespace Parking.Services
{
    public class EventService(ApplicationDbContext context) : IEventService
    {
        private readonly ApplicationDbContext _context = context;

        public ResponseHandler<List<EventHistoryResponseDto>> ListEventHistory(int page, int total, Guid parkId)
        {
            var eventsFineshed = _context.Events
               .Where(e => e.ParkId == parkId && e.GetOutTime != null)
               .Select(e => new EventHistoryResponseDto
               {
                   Id = e.Id,
                   GetInTime = e.GetInTime,
                   GetOutTime = e.GetOutTime,
                   GetInUserId = e.GetInUserId,
                   GetOutUserId = e.GetOutUserId,
                   PaidValueInCents = e.PaidValueInCents,
                   LicensePlate = e.LicensePlate,
                   ParkId = e.ParkId,
                   GetInUserName = _context.Users.FirstOrDefault(p => p.Id == e.GetInUserId).UserName ?? null,
                   GetOutUserName = _context.Users.FirstOrDefault(p => p.Id == e.GetOutUserId).UserName ?? null,
               })
               .Skip((page - 1) * total)
               .Take(total)
               .ToList();

            return new ResponseHandler<List<EventHistoryResponseDto>>(eventsFineshed, null);
        }

        public ResponseHandler<Event> StartParkingEvent(StartParkingDto data)
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

        public ResponseHandler<Event> FinishParkingEvent(FinishParkingDto data)
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

        private static decimal CalculateParkingFee(DateTime entryTime, DateTime exitTime, decimal hourlyRate)
        {
            TimeSpan parkingDuration = exitTime - entryTime;
            double totalHours = Math.Ceiling(parkingDuration.TotalHours);

            decimal totalFeeInCents = Convert.ToDecimal(totalHours) * hourlyRate;

            return totalFeeInCents;
        }
    }
}
