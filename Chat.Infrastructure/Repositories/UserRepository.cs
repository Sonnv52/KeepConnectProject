using Chat.Application.DTOs.UserApp;
using Chat.Application.Helper.Extentions;
using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using Chat.Infrastructure.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<UserApp>, IUserRepository
    {
        private readonly ChatDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserRepository(ChatDbContext chatDbContext, IHttpContextAccessor contextAccessor) : base(chatDbContext)
        {
            _context = chatDbContext;
            _contextAccessor = contextAccessor;
        }

        public async Task<UserApp> GetByStringIdAsync(string id)
        {
           return  await _context.Set<UserApp>().FindAsync(id);
        }

        public Task<PagedList<FriendToList>> GetListFriendAsync(int PageIndex, int PageSize, string? Key)
        {
            var user = _contextAccessor?.HttpContext?.Items["User"] as UserApp;

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var friends = _context.UserApps.Include( u => u.Avatars ).AsQueryable().AsNoTracking();

            if(!string.IsNullOrWhiteSpace(Key))
            {
                friends = friends.Where( f => f.FullName!.Contains(Key));
            }

            friends = friends.Where(f => f != user);

            var friendList = friends.Select(
                f => new FriendToList
                {
                    Email = f.Email,
                    Id = f.Id,
                    Name = f.FullName,
                    Avatar = f.Avatars.Any() ? f.Avatars.First().Path.ReadFile().Resize(240, 320) : null
                }).ToPagedList(PageIndex, PageSize);

                return Task.FromResult(friendList);
        }

        
    }
}
