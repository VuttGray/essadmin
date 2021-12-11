using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Application;
using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.Worker.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESS.Admin.Worker.Services
{
    public class EmailService : MessageService, IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(MailSettings mailSettings, IRepository<Message> repository) : base(repository)
        {
            _mailSettings = mailSettings;
        }

        public async Task SendAsync(Message message)
        {
            // Prepare email
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = message.Subject,
            };
            email.To.Add(MailboxAddress.Parse("denis.stepanov@psi-cro.com"));
            //email.To.AddRange(message.Recipients.Select(r => MailboxAddress.Parse(r)));
            //email.Cc.AddRange(message.CcRecipients.Select(r => MailboxAddress.Parse(r)));
            //email.Bcc.AddRange(message.BccRecipients.Select(r => MailboxAddress.Parse(r)));
            var builder = new BodyBuilder { HtmlBody = message.Body };
            email.Body = builder.ToMessageBody();

            // Send
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.None);
            //smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            await MarkSentAsync(message, DateTime.Now);
            smtp.Disconnect(true);
        }
    }
}
