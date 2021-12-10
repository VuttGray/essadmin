using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Abstractions.Services;
using ESS.Admin.Core.Domain;
using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Admin.Core.Application
{
    public class BaseService<T> : IRepository<T>
        where T: BaseEntity
    {
        protected readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(T entity)
        {
            return _repository.AddAsync(entity);
        }

        public Task DeleteAsync(T entity)
        {
            return _repository.DeleteAsync(entity);
        }

        public Task<IEnumerable<T>> GetActiveAsync()
        {
            return _repository.GetActiveAsync();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<T> GetFirstOrDefaultAsync()
        {
            return _repository.GetFirstOrDefaultAsync();
        }

        public Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            return _repository.GetRangeByIdsAsync(ids);
        }

        public Task UpdateAsync(T entity)
        {
            return _repository.UpdateAsync(entity);
        }
    }
}
