using System.Collections.Generic;
using System.Linq;
using mail.Interfaces;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Pop3;
using Microsoft.AspNetCore.Mvc;

namespace mail.Models
{
    public class EmailService:IEmailService
    {
        public readonly IEmailConfiguration _emailConfiguration;

        public EmailService(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        [HttpGet("receiveEmail")]
        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
            using (var emailClient = new Pop3Client())
            {
                emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

                List<EmailMessage> emails = new List<EmailMessage>();
                for (int i = 0; i < emailClient.Count && i < maxCount; i++)
                {
                    var message = emailClient.GetMessage(i);
                    var emailMessage = new EmailMessage
                    {
                        Message = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };
                    emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress) x)
                        .Select(x => new EmailAddress {Address = x.Address, Name = x.Name}));
                    emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress) x)
                        .Select(x => new EmailAddress {Address = x.Address, Name = x.Name}));
                    emailMessage.CcAddresses.AddRange(message.Cc.Select(x => (MailboxAddress) x)
                        .Select(x => new EmailAddress {Address = x.Address, Name = x.Name}));
                    emailMessage.BccAddresses.AddRange(message.Bcc.Select(x => (MailboxAddress) x)
                        .Select(x => new EmailAddress {Address = x.Address, Name = x.Name}));
                }

                return emails;
            }
        }

        [HttpPost("send")]
        public void Send(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            message.To.AddRange(emailMessage.ToAddresses.Select(c => new MailboxAddress(c.Name, c.Address)));
            message.Cc.AddRange(emailMessage.CcAddresses.Select(c => new MailboxAddress(c.Name, c.Address)));
            message.Bcc.AddRange(emailMessage.BccAddresses.Select(c => new MailboxAddress(c.Name, c.Address)));
            message.From.AddRange(emailMessage.FromAddresses.Select(c => new MailboxAddress(c.Name, c.Address)));

            message.Subject = emailMessage.Subject;

            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Message
            };

            using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
            {
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
        }
    }
}
