using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain;

namespace ESS.Admin.DataAccess.Repositories
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data.AsEnumerable());
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.RecordId == id));
        }

        public Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            return Task.FromResult(Data.Where(x => ids.Contains(x.RecordId)).AsEnumerable());
        }

        public Task AddAsync(T entity)
        {
            return Task.FromResult(Data.Append(entity));
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetActiveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetFirstOrDefaultAsync()
        {
            throw new NotImplementedException();
        }
    }
}