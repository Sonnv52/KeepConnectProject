using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Persistence.Contracts
{
    public interface IGenericRepository<T>: IDisposable where T : class
    {
        Task<T> GetAsync(Guid id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<bool> Exists(Guid id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
