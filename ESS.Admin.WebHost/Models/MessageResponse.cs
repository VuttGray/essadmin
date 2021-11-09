using ESS.Admin.Core.Domain.Administration;
using System;

namespace ESS.Admin.WebHost.Models
{
    public class MessageResponse
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string BodyPreview { get; set; }
        public DateTime? SentDate { get; set; }

        public MessageResponse(Message message)
        {
            Id = message.RecordId;
            Subject = message.Subject;
            BodyPreview = message.BodyPreview;
            SentDate = message.SentDate;
        }
    }
}