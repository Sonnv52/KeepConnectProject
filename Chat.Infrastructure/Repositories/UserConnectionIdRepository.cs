using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs.MongoDbEntities;
using Chat.Infrastructure.Helper.Abtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Repositories
{
    public class UserConnectionIdRepository : MongoRepository<UserConnectionID>, IUserConnectionIdRepository
    {
        private readonly IMongoContext _context;
        public UserConnectionIdRepository(IMongoContext context) : base(context)
        {
            _context = context;
        }
    }
}
