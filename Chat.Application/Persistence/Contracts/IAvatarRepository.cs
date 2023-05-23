using Chat.Domain.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Persistence.Contracts
{
    public interface IAvatarRepository : IGenericRepository<Avatar>
    {
    }
}
