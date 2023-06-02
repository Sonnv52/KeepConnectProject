using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using Chat.Infrastructure.DataContext;

namespace Chat.Infrastructure.Repositories
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private readonly ChatDbContext _context;

        public RoomRepository(ChatDbContext chatDbContext) : base(chatDbContext)
        {
            _context = chatDbContext;
        }
    }
}
