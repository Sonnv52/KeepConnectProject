using Chat.Application.DTOs.UserApp;
using Chat.Application.Helper.Extentions;
using Chat.Domain.DAOs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Persistence.Contracts
{
    public interface IUserRepository : IGenericRepository<UserApp>
    {
        Task<PagedList<FriendToList>> GetListFriendAsync(int PageIndex, int PageSize, string? Key);
    }
}
