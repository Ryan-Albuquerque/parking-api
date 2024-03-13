using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Parking.Models
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)  
    {
        public DbSet<Park> Parks {  get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
