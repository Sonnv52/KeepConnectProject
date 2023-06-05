using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using Chat.Infrastructure.DataContext;

namespace Chat.Infrastructure.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly ChatDbContext _context;

        public MessageRepository(ChatDbContext chatDbContext) : base(chatDbContext)
        {
            _context = chatDbContext;
        }
    }
}
