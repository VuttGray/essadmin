using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESS.Admin.DataAccess.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _dataContext;

        public EfRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task AddAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _dataContext.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<T>> GetActiveAsync()
        {
            var entities = await _dataContext.Set<T>().Where(x => x.RecordStatus == 1).ToListAsync();
            return entities;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.RecordId == id);
            return entity;
        }

        public async Task<T> GetFirstOrDefaultAsync()
        {
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.RecordStatus == 1);
            return entity;
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<Guid> ids)
        {
            var entities = await _dataContext.Set<T>().Where(x => ids.Contains(x.RecordId)).ToListAsync();
            return entities;
        }

        public async Task UpdateAsync(T entity)
        {
            _dataContext.Set<T>().Update(entity);
            await _dataContext.SaveChangesAsync();
        }
    }
}
