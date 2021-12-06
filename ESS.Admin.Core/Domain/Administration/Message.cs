using System;
using System.Collections.Generic;
using System.Linq;

namespace ESS.Admin.Core.Domain.Administration
{
    public class Message : BaseEntity
    {
        public string Subject { get; private set; }
        public string Body { get; private set; }
        public string BodyPreview => Body.Length > 100 ? Body.Substring(0, 100) : Body;
        public List<string> Recipients { get; private set; } = new List<string>();
        public List<string> CcRecipients { get; private set; } = new List<string>();
        public List<string> BccRecipients { get; private set; } = new List<string>();
        public List<string> Attachments { get; private set; } = new List<string>();
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public int Attempts { get; private set; } = 0;
        public DateTime? SentDate { get; private set; }

        private readonly Dictionary<string, string> _context;

        public Message(Guid id,
            string subject, 
            string body, 
            Dictionary<string, string> context = null)
        {
            RecordId = id;
            _context = context;
            Subject = ProcessContext(subject);
            Body = ProcessContext(body);
        }

        private string ProcessContext(string text)
        {
            if (!text.Contains("$") || _context == null) return text;

            // Sort by length desc to avoid replacing part of a key with another shorter key: $KEY instead of $KEY_OTHER
            var keys = _context.Keys.OrderByDescending(x => x.Length);  
            foreach (string key in keys)
            {
                text = text.Replace($"${key}", _context[key]).Replace(";", "").Replace(",", "").Trim();
            }
            return text;
        }

        public void AddRecipients(string recipients)
        {
            AddRecipients(recipients.Split(";"));
        }

        public void AddRecipients(IEnumerable<string> recipients)
        {
            AddRecipients(Recipients, recipients);
        }

        public void AddCcRecipients(string recipients)
        {
            AddCcRecipients(recipients.Split(";"));
        }

        public void AddCcRecipients(IEnumerable<string> recipients)
        {
            AddRecipients(CcRecipients, recipients);
        }

        public void AddBccRecipients(string recipients)
        {
            AddBccRecipients(recipients.Split(";"));
        }

        public void AddBccRecipients(IEnumerable<string> recipients)
        {
            AddRecipients(BccRecipients, recipients);
        }

        public void IncrementAttempts()
        {
            Attempts += 1;
        }

        public void MarkSent(DateTime sentDate)
        {
            IncrementAttempts();
            SentDate = sentDate;
        }

        protected void AddRecipients(List<string> list, IEnumerable<string> recipients)
        {
            foreach (var recipient in recipients)
            {
                var processed_recipient = ProcessContext(recipient);
                if (string.IsNullOrEmpty(processed_recipient)) continue;
                if (!list.Contains(processed_recipient)) list.Add(processed_recipient);
            }
        }
    }
}