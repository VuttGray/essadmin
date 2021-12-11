using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Admin.Worker.Services
{
    public interface IEmailService : IRepository<Message>
    {
        Task SendAsync(Message message);
    }
}
