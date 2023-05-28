using Chat.Application.Exceptions;
using Chat.Application.Persistence.Contracts;
using Chat.Infrastructure.DataContext;

namespace Chat.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _context;
        private IAvatarRepository _avatarRepository;

        public UnitOfWork(ChatDbContext context)
        {
            _context= context;
        }

        public IAvatarRepository AvatarRepository =>
            _avatarRepository ??= new AvatarRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new SqlException(ex.ToString());
            }
        }
    }
}
