using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Admin.Core.Abstractions.Services
{
    public interface IUserService : IRepository<User>
    {
        Task LockAsync(User user);
    }
}
