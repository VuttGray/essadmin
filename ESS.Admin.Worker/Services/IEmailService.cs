using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Admin.Worker.Services
{
    public interface IEmailService
    {
        Task SendAsync(Message message);
    }
}
