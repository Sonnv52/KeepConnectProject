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

        public override Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                entry.Entity.LastModifiedDate = DateTime.Now;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellation);
        }

        public DbSet<UserApp>? UserApps { get; set; }
        public DbSet<Avatar>? Avatars { get; set; }
        public DbSet<Image>? Images { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<UserRoom>? UserRooms { get; set;}
        public DbSet<Message>? Messages { get; set; }
    }
}
