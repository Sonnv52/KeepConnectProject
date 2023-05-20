using Chat.Application.DTOs.UserApp;
using Chat.Application.Respone;
using Chat.Domain.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Persistence.Contracts
{
    public interface IUserRepository : IGenericRepository<UserApp>
    {
        Task<UserApp> GetByEmailAsync(string email);
        Task<BaseCommandResponse<bool>> SignUpAsync(UserDTO user);
    }
}
