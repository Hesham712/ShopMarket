using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Reprository.Interface
{
    public interface ITokenService
    {
        Task<string> CreateToken(User user);
    }
}
