using System;

namespace ESS.Admin.Core.Domain.Administration
{
    public class Message : BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BodyPreview => Body.Length > 100 ? Body.Substring(0, 100) : Body;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? SentDate { get; set; }
        public int Attempts { get; set; } = 0;
    }
}