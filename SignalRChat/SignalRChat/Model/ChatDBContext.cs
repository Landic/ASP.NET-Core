using Microsoft.EntityFrameworkCore;

namespace SignalRChat.Model
{
    public class ChatDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Messages> Message { get; set; }

        public ChatDBContext(DbContextOptions<ChatDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
