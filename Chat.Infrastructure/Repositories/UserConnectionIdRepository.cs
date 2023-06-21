using Chat.Application.Persistence.Contracts;
using Chat.Domain.DAOs.MongoDbEntities;
using Chat.Infrastructure.Helper.Abtractions;

namespace Chat.Infrastructure.Repositories
{
    public class UserConnectionIdRepository : MongoRepository<UserConnectionID>, IUserConnectionIdRepository
    {
        public UserConnectionIdRepository(IMongoContext context) : base(context)
        {
        }

        //Remove by id connection
          public async override Task<bool> RemoveAsync(string id)
        {
            var result = await DbSet.DeleteOneAsync(FilterByConnectionId(id));
            return result.IsAcknowledged;
        }
    }
}
