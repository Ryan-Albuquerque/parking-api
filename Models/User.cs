using Microsoft.AspNetCore.Identity;

namespace Parking.Models
{
    public class User : IdentityUser
    {
        public Guid ParkId { get; set; }
    }
}
