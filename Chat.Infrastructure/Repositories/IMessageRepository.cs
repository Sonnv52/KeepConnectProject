using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
    }
}
