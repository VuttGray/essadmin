using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Abstractions.Services;
using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Admin.Core.Application
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        public MessageService(IRepository<Message> repository) : base(repository) { }

        public async Task<IEnumerable<Message>> GetMessagesToSendAsync()
        {
            var entities = await _repository.GetActiveAsync();
            return entities;
        }

        public async Task MarkSentAsync(Message message, DateTime sentDate)
        {
            message.MarkSent(sentDate);
            await _repository.UpdateAsync(message);
        }
    }
}
