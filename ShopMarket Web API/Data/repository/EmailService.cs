using Microsoft.Extensions.Options;
using MimeKit;
using ShopMarket_Web_API.Configuration;
using ShopMarket_Web_API.Data.Interface;
using ShopMarket_Web_API.Models;
using System.Net;
using System.Net.Mail;

namespace ShopMarket_Web_API.Data.repository
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML)
        {
            string MailServer = _config["EmailSettings:MailServer"];
            string UserName = _config["EmailSettings:UserName"];
            string FromEmail = _config["EmailSettings:FromEmail"];
            string Password = _config["EmailSettings:Password"];
            int Port = int.Parse(_config["EmailSettings:MailPort"]);

            var client = new SmtpClient(MailServer, Port)
            {
                Credentials = new NetworkCredential(UserName, Password),
                EnableSsl = false,
            };
            MailMessage mailMessage = new MailMessage(FromEmail, toEmail, subject, body)
            {
                IsBodyHtml = isBodyHTML
            };
            await client.SendMailAsync(mailMessage);
        }
    }
}
