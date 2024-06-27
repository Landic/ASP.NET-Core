using Microsoft.EntityFrameworkCore;

namespace GuestBook.Models
{
    public class GuestBookContext : DbContext
    {
        public DbSet<User> Users { get; set;}
        public DbSet<Messages> Messages { get; set;}
        public GuestBookContext(DbContextOptions<GuestBookContext> options) : base(options) 
        { 
            Database.EnsureCreated();
        }
    }
}
