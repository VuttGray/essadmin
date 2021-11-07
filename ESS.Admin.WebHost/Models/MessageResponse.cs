using System;

namespace ESS.Admin.WebHost.Models
{
    public class MessageResponse
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string BodyPreview { get; set; }
        public DateTime? SentDate { get; set; }
    }
}