using Chat.Domain.DAOs;
using Chat.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
