using Chat.Domain.DAO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.DataContext
{
    public class ChatDbContext : IdentityDbContext<UserApp>
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) 
            : base(options) 
        {
        }

        public DbSet<UserApp>? UserApps { get; set; }  
        public DbSet<Avatar>? Avatars { get; set; }
        public DbSet<Image>? Images { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<BillDetail>().ToTable(nameof(BillDetail), p => p.IsTemporal());
        }
    }
}
