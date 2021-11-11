using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.WebHost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESS.Admin.WebHost.Mappers
{
    public class MessageMapper : IMessageMapper
    {
        public Message MapFromModel(CreateMessageRequest model, Message message = null)
        {
            if (message == null)
            {
                message = new Message() { RecordId = Guid.NewGuid() };
            }

            message.Subject = model.Subject;
            message.Body = model.Body;
            message.CreatedDate = DateTime.Now;
            message.Attempts = 0;

            return message;
        }
    }
}
