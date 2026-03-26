using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;


namespace basic_authentication.Services
{
    public class EmailSender : IEmailSender
    {
       public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Implement your email sending logic here
            // For example, you can use an SMTP client or a third-party email service API
            // This is just a placeholder implementation
            Console.WriteLine($"Sending email to {email} with subject '{subject}' and message: {htmlMessage}");
            var smtpClient = new SmtpClient("smtp.ethereal.email")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("rod.tillman@ethereal.email", "AeqN6FysU5p98B4Jst")
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("rod.tillman@ethereal.email", "Todoapp test"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml=true
            };

            mailMessage.To.Add(email);
            await smtpClient.SendMailAsync(mailMessage);
            return;
        }
    }
}

