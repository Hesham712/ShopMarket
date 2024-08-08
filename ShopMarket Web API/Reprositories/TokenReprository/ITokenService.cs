using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Reprository.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
