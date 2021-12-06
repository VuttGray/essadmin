using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Abstractions.Services;
using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Admin.Core.Application
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(User entity)
        {
            return _repository.AddAsync(entity);
        }

        public Task DeleteAsync(User entity)
        {
            return _repository.DeleteAsync(entity);
        }

        public Task<IEnumerable<User>> GetActiveAsync()
        {
            return _repository.GetActiveAsync();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IEnumerable<User>> GetRangeByIdsAsync(List<Guid> ids)
        {
            return _repository.GetRangeByIdsAsync(ids);
        }

        public Task LockAsync(User user)
        {
            user.IsLocked = true;
            return _repository.UpdateAsync(user);
        }

        public Task UpdateAsync(User entity)
        {
            return _repository.UpdateAsync(entity);
        }
    }
}
