using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Abstractions.Services;
using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ESS.Admin.Core.Application
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> _repository;

        public MessageService(IRepository<Message> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(Message entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Message entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetActiveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Message>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Message> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Message>> GetMessagesToSendAsync()
        {
            var entities = await _repository.GetActiveAsync();
            return entities;
        }

        public Task<IEnumerable<Message>> GetRangeByIdsAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public async Task MarkSentAsync(Message message, DateTime sentDate)
        {
            message.MarkSent(sentDate);
            await _repository.UpdateAsync(message);
        }

        public Task UpdateAsync(Message entity)
        {
            throw new NotImplementedException();
        }
    }
}
