using HandmadeShop.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace HandmadeShop.Infrastructure.Provider
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailSettings:SenderEmail"]));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            var builder = new BodyBuilder { HtmlBody = body };
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            try
            {
                await smtp.ConnectAsync(_config["EmailSettings:Host"], int.Parse(_config["EmailSettings:Port"]), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["EmailSettings:SenderEmail"], _config["EmailSettings:Password"]);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi gửi mail: {ex.Message}");
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}