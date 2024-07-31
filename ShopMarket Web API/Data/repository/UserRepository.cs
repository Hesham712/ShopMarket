using AutoMapper;
using Finance_WebApi.Dtos.Account;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopMarket_Web_API.Data.Interface;
using ShopMarket_Web_API.Dtos.Account;
using ShopMarket_Web_API.Dtos.Order;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Data.repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserRepository(IEmailService emailService, UserManager<User> userManager, IMapper mapper, ApplicationDbContext context)
        {
            _emailService = emailService;
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> ChangePasswordAsync(int UserId, UpdateUserPasswordDto userPassDto)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user != null)
            {
                var oldPassExist = await _userManager.CheckPasswordAsync(user,userPassDto.OldPassword);
                if (oldPassExist)
                {
                    if (userPassDto.NewPassword == userPassDto.ConfirmPassword)
                    {
                        await _userManager.ChangePasswordAsync(user, userPassDto.OldPassword, userPassDto.NewPassword);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> ConfirmEmail(int userId, string token)
        {
            var userDetails = await _userManager.FindByIdAsync(userId.ToString());
            if (userId == null || token == null)
                return "Link expired";
            else if (userDetails == null)
                return "User not found";
            else
            {
                var result = await _userManager.ConfirmEmailAsync(userDetails, token);
                if (result.Succeeded)
                    return "Thank you for confirming your email";
                return "Email not confirmed";
            }
        }

        public async Task<User> CreateUser(SignUpUserDto userModel)
        {
            var userExist = await _userManager.FindByEmailAsync(userModel.Email);

            if (userExist is not null)
                throw new ArgumentException("Email is taken");

            var user = _mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _emailService.SendEmailAsync(userModel.Email, "Confirm Your Email", $"Please confirm your account YOur token : {emailConfirmationToken}", true);
                return (user);
            }
            throw new ArgumentException(result.ToString());

        }

        public async Task<bool> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var result = await _userManager.SetLockoutEnabledAsync(user, true);
                if (result.Succeeded)
                    return true;
            }
            return false;
        }

        public async Task<IList<User>> GetActiveUserAsync() =>
            await _userManager.Users.Where(m => m.LockoutEnabled == false).ToListAsync();

        public async Task<IList<User>> GetInActiveUserAsync() =>
            await _userManager.Users.Where(m => m.LockoutEnabled == true).ToListAsync();

        public async Task<UserGetDto> UpdateUser(int UserId, UpdateUserDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == UserId);
            if (user == null)
                return null;

            _mapper.Map<UpdateUserDto, User>(userDto, user);
            var userGetDto = _mapper.Map<UserGetDto>(user);

            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();
            return userGetDto;
        }
    }
}
