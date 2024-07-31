using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Data.Interface
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML);
    }
}
