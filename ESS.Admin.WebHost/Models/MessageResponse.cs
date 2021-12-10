using ESS.Admin.Core.Domain.Administration;
using System;
using System.Collections.Generic;

namespace ESS.Admin.WebHost.Models
{
    public class MessageResponse
    {
        public Guid RecordId { get; set; }
        public string Subject { get; set; }
        public string BodyPreview { get; set; }
        public List<string> Recipients { get; set; }
        public DateTime? SentDate { get; set; }
    }
}