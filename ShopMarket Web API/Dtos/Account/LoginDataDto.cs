using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Dtos.Account
{
    public class LoginDataDto
    {
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
