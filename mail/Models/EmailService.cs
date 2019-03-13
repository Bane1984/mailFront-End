//using System.Collections.Generic;
//using System.Linq;
//using mail.Interfaces;
//using MimeKit;
//using MimeKit.Text;
//using MailKit.Net.Pop3;
//using Microsoft.AspNetCore.Mvc;

//namespace mail.Models
//{
//    public class EmailService:IEmailService
//    {
//        public readonly IEmailConfiguration _emailConfiguration;

//        public EmailService(IEmailConfiguration emailConfiguration)
//        {
//            _emailConfiguration = emailConfiguration;
//        }

//        //[HttpGet("receiveEmail")]
//        //public List<EmailMessage> ReceiveEmail(int maxCount = 10)
//        //{
//        //    using (var emailClient = new Pop3Client())
//        //    {
//        //        emailClient.Connect(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

//        //        emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

//        //        emailClient.Authenticate(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

//        //        List<EmailMessage> emails = new List<EmailMessage>();
//        //        for (int i = 0; i < emailClient.Count && i < maxCount; i++)
//        //        {
//        //            var message = emailClient.GetMessage(i);
//        //            var emailMessage = new EmailMessage
//        //            {
//        //                Message = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
//        //                Subject = message.Subject
//        //            };
//        //            emailMessage.To.AddRange(message.To.Select(x => (MailboxAddress) x)
//        //                .Select(x => new EmailAddress { Email = x.Address}));
//        //            emailMessage.Email.AddRange(message.From.Select(x => (MailboxAddress) x)
//        //                .Select(x => new EmailAddress { Email = x.Address}));
//        //            emailMessage.Cc.AddRange(message.Cc.Select(x => (MailboxAddress) x)
//        //                .Select(x => new EmailAddress { Email = x.Address}));
//        //            emailMessage.Bcc.AddRange(message.Bcc.Select(x => (MailboxAddress) x)
//        //                .Select(x => new EmailAddress { Email = x.Address}));
//        //        }

//        //        return emails;
//        //    }
//        //}

//        public void Send(EmailMessage emailMessage)
//        {
            
//            var message = new MimeMessage();
//            message.To.AddRange(emailMessage.To.Select(c => new MailboxAddress(c.Email)));
//            message.Cc.AddRange(emailMessage.Cc.Select(c => new MailboxAddress(c.Email)));
//            message.Bcc.AddRange(emailMessage.Bcc.Select(c => new MailboxAddress(c.Email)));
//            message.From.Add(new MailboxAddress(emailMessage.Email, emailMessage.Name));
            
//            message.Subject = emailMessage.Subject;

//            message.Body = new TextPart(TextFormat.Html)
//            {
//                Text = emailMessage.Message
//            };

//            using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
//            {
//                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
//                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
//                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
//                emailClient.Send(message);
//                emailClient.Disconnect(true);
//            }

//        }

       
//    }
//}
