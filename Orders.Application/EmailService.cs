using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Threading.Tasks;

namespace Orders.Application
{
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string recipient, string subject, string body, byte[] attachmentData)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("konstmavrtest@gmail.com");
                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.Body = body;

                if (attachmentData != null)
                {
                    Attachment attachment = new Attachment(new MemoryStream(attachmentData), "attachment.pdf");
                    mail.Attachments.Add(attachment);
                }

                using (SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential("konstmavrtest@gmail.com", "mqtniosvadjjabvb")
                })
                {
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}
