using System;
using System.Threading.Tasks;

namespace DynamicAuthorization.Utils
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Console.Out.WriteLineAsync("Email sent!");
        }
    }


}
