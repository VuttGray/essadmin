using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESS.Admin.Core.Application.Services
{
    public class BaseService<T> : IRepository<T>
        where T: BaseEntity
    {
        protected readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<T>> GetActiveAsync()
        {
            return await _repository.GetActiveAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        {
            return await _repository.GetAllAsync(ct);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<T> GetFirstOrDefaultAsync()
        {
            return await _repository.GetFirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            return await _repository.GetRangeByIdsAsync(ids);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }
    }
}
