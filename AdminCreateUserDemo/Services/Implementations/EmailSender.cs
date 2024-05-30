using AdminCreateUserDemo.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace AdminCreateUserDemo.Services.Implementations
{
    public sealed class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
            {
                Port = int.Parse(_configuration["Smtp:Port"]),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
                EnableSsl = bool.Parse(_configuration["Smtp:EnableSsl"]),
            };
           
            var emailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Smtp:From"]),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            emailMessage.To.Add(email);

            try
            {
                await smtpClient.SendMailAsync(emailMessage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Email sending failed.", ex);
            }
        }
    }
}
