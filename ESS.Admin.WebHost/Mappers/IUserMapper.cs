using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESS.Admin.WebHost.Mappers
{
    public interface IUserMapper
    {
        public User MapFromModel(CreateOrEditUserRequest model, User user = null);
    }
}
