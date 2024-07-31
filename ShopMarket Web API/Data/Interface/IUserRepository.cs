using Finance_WebApi.Dtos.Account;
using ShopMarket_Web_API.Dtos.Account;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Data.Interface
{
    public interface IUserRepository
    {
        public Task<User> CreateUser(SignUpUserDto user);
        public Task<IList<User>> GetActiveUserAsync();
        public Task<IList<User>> GetInActiveUserAsync();
        public Task<bool> DeleteUser(string userName);
        public Task<UserGetDto> UpdateUser(int UserId,UpdateUserDto user);
        public Task<bool> ChangePasswordAsync(int UserId, UpdateUserPasswordDto userPassDto);
        public Task<string> ConfirmEmail(int userId,string token);
    }
}
