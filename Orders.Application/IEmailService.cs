using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string recipient, string subject, string body, string attachmentPath);
    }
}
