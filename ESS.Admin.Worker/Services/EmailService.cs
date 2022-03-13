using ESS.Admin.Core.Abstractions.Repositories;
using ESS.Admin.Core.Application.Services;
using ESS.Admin.Core.Domain.Administration;
using ESS.Admin.Worker.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ESS.Admin.Worker.Services
{
    public class EmailService : MessageService, IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly TelegramBotClient _tgClient;

        public EmailService(MailSettings mailSettings, IRepository<Message> repository, TelegramBotClient tgClient) 
            : base(repository)
        {
            _mailSettings = mailSettings;
            _tgClient = tgClient;
        }

        public async Task SendAsync(Message message)
        {
            if (message.Recipients.Count > 0 && int.TryParse(message.Recipients[0], out var _))
                await SendTgMessageAsync(message);
            else
                await SendEmailAsync(message);
            await MarkSentAsync(message, DateTime.Now);
        }

        private async Task SendTgMessageAsync(Message message)
        {
            foreach (var recipient in message.Recipients)
                await _tgClient.SendTextMessageAsync(recipient, message.Body);
        }

        public async Task SendEmailAsync(Message message)
        {
            // Prepare email
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail),
                Subject = message.Subject,
            };
            if (message.Recipients != null && message.Recipients.Count > 0)
            {
                email.To.AddRange(message.Recipients.Select(r => MailboxAddress.Parse(r)));
                email.Cc.AddRange(message.CcRecipients.Select(r => MailboxAddress.Parse(r)));
                email.Bcc.AddRange(message.BccRecipients.Select(r => MailboxAddress.Parse(r)));
            }
            else
            {
                email.To.Add(MailboxAddress.Parse(_mailSettings.DefaultRecipient));
            }
            var builder = new BodyBuilder { HtmlBody = message.Body };
            email.Body = builder.ToMessageBody();

            CheckToSend(message);
            // Send
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.None);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
