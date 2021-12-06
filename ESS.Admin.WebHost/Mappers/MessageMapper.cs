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
                message = new Message(Guid.NewGuid(), model.Subject, model.Body);
            }

            return message;
        }
    }
}
