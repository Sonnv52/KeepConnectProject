namespace Chat.Application.Persistence.Contracts
{
    public interface IGenericRepositoryMongoDb<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> UpdateAsync(string id, TEntity obj);
        Task<bool> RemoveAsync(string id);
    }
}
