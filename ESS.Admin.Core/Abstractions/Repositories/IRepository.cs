using ESS.Admin.Core.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESS.Admin.Core.Abstractions.Repositories
{
    public interface IRepository<T> where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetActiveAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
