using Parking.Models;

namespace Parking.Data
{
    public static class Seeders
    {
        private static readonly Guid parkId = new();

        private static readonly Guid userA = new();
        private static readonly Guid userB = new();

        private static readonly DateTime currentDate = DateTime.UtcNow;
        public static void Initialize(ApplicationDbContext context)
        {
            VerifyTables(context);

            List<Event> events =
            [
                new ()
                {
                    Id = Guid.NewGuid(),
                    GetInTime = currentDate.AddHours(-1),
                    GetOutTime = currentDate.AddMinutes(-30),
                    GetInUserId = userA,
                    GetOutUserId = userA,
                    LicensePlate = "ABC123",
                    ParkId = parkId,
                    PaidValueInCents = 5000
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    GetInTime = currentDate.AddHours(-1),
                    GetOutTime = currentDate.AddMinutes(-30),
                    GetInUserId = userA,
                    GetOutUserId = userA,
                    LicensePlate = "ABC123",
                    ParkId = parkId,
                    PaidValueInCents = 5000
                },
            ];

            List<Park> parks =
            [
                new ()
                {
                    Id = parkId,
                    Name = "Seu João",
                    PricePerHourInCents = 600
                },
                new ()
                {
                    Id = new Guid(),
                    Name = "Seu João",
                    PricePerHourInCents = 600
                }
            ];

            List<User> users = 
            [
                new() 
                {
                    Id = userA.ToString(),
                    ParkId = parkId
                },
                new()
                {
                    Id = userB.ToString(),
                    ParkId = parkId
                }
            ];

            context.Events.AddRange(events);
            context.Parks.AddRange(parks);
            context.Users.AddRange(users);

            context.SaveChanges();
        }

        private static void VerifyTables(ApplicationDbContext context)
        {
            if (context.Events.Any())
            {
                return;
            }
            if (context.Parks.Any())
            {
                return;
            }
            if (context.Users.Any())
            {
                return;
            }
        }
    }
}
