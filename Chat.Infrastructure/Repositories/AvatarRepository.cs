using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using Chat.Infrastructure.DataContext;

namespace Chat.Infrastructure.Repositories
{
    public class AvatarRepository : GenericRepository<Avatar>, IAvatarRepository
    {
        private readonly ChatDbContext _context;
        public AvatarRepository(ChatDbContext chatDbContext) : base(chatDbContext)
        {
            _context = chatDbContext;
        }
    }
}
