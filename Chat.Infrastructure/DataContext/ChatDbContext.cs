using Chat.Domain.Common;
using Chat.Domain.DAOs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.DataContext
{
    public class ChatDbContext : IdentityDbContext<UserApp>
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChatDbContext).Assembly);

            modelBuilder.Entity<Room>()
           .HasMany(r => r.Messages)
           .WithOne(m => m.Room)
           .OnDelete(DeleteBehavior.Cascade);
        }

        public virtual async Task<int> SaveChangesAsync( CancellationToken token, string username = "SYSTEM")
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                entry.Entity.LastModifiedBy = username;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = username;
                }
            }

            var result = await base.SaveChangesAsync(token);

            return result;
        }

        public DbSet<UserApp>? UserApps { get; set; }
        public DbSet<Avatar>? Avatars { get; set; }
        public DbSet<Image>? Images { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<UserRoom>? UserRooms { get; set;}
        public DbSet<Message>? Messages { get; set; }
    }
}
