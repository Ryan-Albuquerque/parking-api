using Microsoft.AspNetCore.Identity;

namespace Parking.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Guid ParkId { get; set; }
    }
}
