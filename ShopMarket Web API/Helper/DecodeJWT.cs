using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace ShopMarket_Web_API.Helper
{
    public static class DecodeJWT 
    {
        public async static Task<string> GetUserNameFromToken(HttpContext context)
        {
            var Token = await context.GetTokenAsync("access_token");
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(Token);
            var userName = jwtSecurityToken.Claims.First(claim => claim.Type == "unique_name").Value;
            return userName;
        }
        public async static Task<string> GetEmailFromToken(HttpContext context)
        {
            var Token = await context.GetTokenAsync("access_token");
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(Token);
            var Email = jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value;
            return Email;
        }

    }
}
