using Chat.Application.Exceptions;
using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs;
using Chat.Infrastructure.DataContext;
using Chat.Infrastructure.Helper.Abtractions;
using Microsoft.AspNetCore.Http;

namespace Chat.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _context;
        private IAvatarRepository _avatarRepository;
        private IMessageRepository _messageRepository;
        private IUserRepository _userRepository;
        private IUserRoomRepository _userRoomRepository;
        private IHttpContextAccessor _httpContext;
        private IUserConnectionIdRepository _userConnectionIdRepository;
        private IMongoContext _mongoDbContext; 

        public UnitOfWork(ChatDbContext context, IHttpContextAccessor httpContext,
            IMongoContext mongoDbContext)
        {
            _context = context;
            _httpContext = httpContext;
            _mongoDbContext = mongoDbContext;
        }

        public IAvatarRepository AvatarRepository =>
            _avatarRepository ??= new AvatarRepository(_context);

        public IUserRepository UserRepository =>
           _userRepository ??= new UserRepository(_context, _httpContext);

        public IUserRoomRepository UserRoomRepository => 
            _userRoomRepository ??= new UserRoomRepository(_context);

        public IMessageRepository MessageRepository =>
            _messageRepository ??= new MessageRepository(_context);

        public IUserConnectionIdRepository UserConnectionIdRepository =>
            _userConnectionIdRepository ??= new UserConnectionIdRepository(_mongoDbContext);

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
