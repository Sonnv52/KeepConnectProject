using Chat.Application.Exceptions;
using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using Chat.Infrastructure.DataContext;
using Microsoft.AspNetCore.Http;

namespace Chat.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _context;
        private IAvatarRepository _avatarRepository;
        private IUserRepository _userRepository;
        private IHttpContextAccessor _httpContext;

        public UnitOfWork(ChatDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public IAvatarRepository AvatarRepository =>
            _avatarRepository ??= new AvatarRepository(_context);

        public IUserRepository UserRepository =>
           _userRepository ??= new UserRepository(_context, _httpContext);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task CommitAsync(CancellationToken token)
        {
            try
            {
                var userName = _httpContext?.HttpContext?.Items["User"] as UserApp;

                if (userName == null)
                    userName = new UserApp
                    {
                        Email = "SYSTEM"
                    };

                string name = userName.Email ?? " ";
                await _context.SaveChangesAsync(token, name);
            }
            catch (Exception ex)
            {
                throw new SqlException(ex.ToString());
            }
        }
    }
}
