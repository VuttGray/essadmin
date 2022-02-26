using System;
using System.Collections.Generic;
using System.Linq;

namespace ESS.Admin.Core.Domain.Administration
{
    public enum MessagePriority
    {
        Low = 0,
        Normal = 1,
        High = 2
    }

    public class Message : BaseEntity
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BodyPreview => Body != null && Body.Length > 100 ? Body.Substring(0, 100) : Body;
        public List<string> Recipients { get; set; } = new List<string>();
        public string RecipientsList => string.Join("; ", Recipients);
        public List<string> CcRecipients { get; set; } = new List<string>();
        public string CcRecipientsList => string.Join("; ", CcRecipients);
        public List<string> BccRecipients { get; set; } = new List<string>();
        public string BccRecipientsList => string.Join("; ", BccRecipients);
        public List<string> Attachments { get; set; } = new List<string>();
        public MessagePriority Priority { get; set; } = MessagePriority.Normal;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int Attempts { get; set; } = 0;
        public DateTime? SentDate { get; set; }

        public void SetContext(Dictionary<string, string> context)
        {
            Subject = ProcessContext(Subject, context);
            Body = ProcessContext(Body, context);
            Recipients = Recipients.Select(r => ProcessContext(r, context)).ToList();
            CcRecipients = CcRecipients.Select(r => ProcessContext(r, context)).ToList();
            BccRecipients = BccRecipients.Select(r => ProcessContext(r, context)).ToList();
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
            RecordStatus = 3;
        }

        public void Reset()
        {
            SentDate = null;
            RecordStatus = 1;
        }

        protected void AddRecipients(List<string> list, IEnumerable<string> recipients)
        {
            foreach (var recipient in recipients)
            {
                if (string.IsNullOrEmpty(recipient)) continue;
                if (!list.Contains(recipient)) list.Add(recipient);
            }
        }

        protected string ProcessContext(string text, Dictionary<string, string> context)
        {
            if (!text.Contains("$") || context == null) return text;

            // Sort by length desc to avoid replacing part of a key with another shorter key: $KEY instead of $KEY_OTHER
            var keys = context.Keys.OrderByDescending(x => x.Length);
            foreach (string key in keys)
            {
                text = text.Replace($"${key}", context[key]);
            }
            return text;
        }
    }
}