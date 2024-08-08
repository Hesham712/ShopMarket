using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Reprository.EmailReprository
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML);
    }
}
