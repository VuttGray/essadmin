using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Abstractions.Services;
using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Admin.Core.Application
{
    public class UserService : BaseService<User>, IUserService
    {

        public UserService(IRepository<User> repository) : base(repository) { }

        public Task LockAsync(User user)
        {
            user.IsLocked = true;
            return _repository.UpdateAsync(user);
        }
    }
}
