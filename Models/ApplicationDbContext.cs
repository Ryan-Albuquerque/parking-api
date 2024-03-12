using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Parking.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  
    {
        public DbSet<Park> Parks {  get; set; }
        public DbSet<Event> Events { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
