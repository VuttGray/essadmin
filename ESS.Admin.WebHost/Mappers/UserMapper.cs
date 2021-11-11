using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESS.Admin.WebHost.Mappers
{
    public class UserMapper : IUserMapper
    {
        public User MapFromModel(CreateOrEditUserRequest model, User user = null)
        {
            if (user == null)
            {
                user = new User() { RecordId = Guid.NewGuid() };
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.RecordStatus = model.RecordStatus;

            return user;
        }
    }
}
