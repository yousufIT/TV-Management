using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace TV.MVC.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            Console.WriteLine($"To: {email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {htmlMessage}");

            return Task.CompletedTask;
        }
    }
}
