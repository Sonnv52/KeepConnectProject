using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IAvatarRepository AvatarRepository { get; }
        Task CommitAsync();
    }
}
