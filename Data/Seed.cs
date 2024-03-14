using Microsoft.EntityFrameworkCore;
using Parking.Models;

namespace Parking.Data
{
    public static class Seeders
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            var parkId = Guid.NewGuid();

            var userA = Guid.NewGuid().ToString();
            var userB = Guid.NewGuid().ToString();

            DateTime currentDate = DateTime.UtcNow;

            var hasData = await HasDataInTables(context);
            if(hasData)
            {
                return;
            }

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
                    Name = "Seu João Park",
                    PricePerHourInCents = 600
                },
                new ()
                {
                    Id = new Guid(),
                    Name = "Seu Pedro Park",
                    PricePerHourInCents = 600
                }
            ];

            List<User> users =
            [
                new()
                {
                    Id = userA,
                    ParkId = parkId,
                    UserName = "chico"

                },
                new()
                {
                    Id = userB,
                    ParkId = parkId,
                    UserName = "tony"
                }
            ];

            await context.Events.AddRangeAsync(events);
            await context.Parks.AddRangeAsync(parks);
            await context.Users.AddRangeAsync(users);

            await context.SaveChangesAsync();
        }

        private static async Task<bool> HasDataInTables(ApplicationDbContext context)
        {
            System.Console.WriteLine(context.Parks.ToArray());
            if (await context.Events.AnyAsync())
            {
                return true;
            }
            if (await context.Parks.AnyAsync())
            {
                return true;
            }
            if (await context.Users.AnyAsync())
            {
                return true;
            }

            return false;
        }
    }
}
