using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Data.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
