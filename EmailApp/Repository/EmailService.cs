using EmailApp.IRepository;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace EmailApp.Repository
{
    public class EmailService : IEmailService
    {
        public readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> SendEmail(string ToEmail, string MailSubject, string MailBody)
        {
            try
            {
                // Direct access to configuration values
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var port = int.Parse(_configuration["EmailSettings:Port"]);
                var FromMail = _configuration["EmailSettings:Username"];
                var password = _configuration["EmailSettings:Password"];

                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Your Name", FromMail));
                email.To.Add(new MailboxAddress("To Name", ToEmail));
                email.Subject = MailSubject;
                email.Body = new TextPart("html") { Text = MailBody };

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(smtpServer, port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(FromMail, password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return "Email sent successfully!";
            }
            catch (Exception ex)
            {
                throw; 
            }
        }
    }
}
