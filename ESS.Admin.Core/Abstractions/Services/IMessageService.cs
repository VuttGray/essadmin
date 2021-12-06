using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESS.Admin.Core.Abstractions.Services
{
    public interface IMessageService : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessagesToSendAsync();
        Task MarkSentAsync(Message message, DateTime sentDate);
    }
}
