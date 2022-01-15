using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Abstractions.Services;
using ESS.Admin.Core.Application.Exceptions;
using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Admin.Core.Application.Services
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        public MessageService(IRepository<Message> repository) : base(repository) { }

        public new async Task<Message> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new EntityNotFoundException("Message", id.ToString());
            if (entity.SentDate != null) throw new EntityBadOperationException("Message", id.ToString(), "is already sent (sent date is filled)");
            if (entity.RecordStatus == 3) throw new EntityBadOperationException("Message", id.ToString(), "status is already sent");

            return entity;
        }

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
