using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string recipient, string subject, string body, string attachmentPath)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("konstmavr@example.com");
                mail.To.Add(recipient);
                mail.Subject = subject;
                mail.Body = body;

                Attachment attachment = new Attachment(attachmentPath);
                mail.Attachments.Add(attachment);

                using (SmtpClient smtp = new SmtpClient("smtp.mail.yahoo.com", 465))
                {
                    smtp.Credentials = new System.Net.NetworkCredential("your-email@example.com", "your-password");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}
