using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace TV.MVC.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Log email details to the console
            Console.WriteLine($"To: {email}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {htmlMessage}");

            // Integrate with an actual email service here

            return Task.CompletedTask;
        }
    }
}
