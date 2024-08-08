using AutoMapper;
using Finance_WebApi.Dtos.Account;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopMarket_Web_API.Data;
using ShopMarket_Web_API.Dtos.Account;
using ShopMarket_Web_API.Dtos.Order;
using ShopMarket_Web_API.Models;
using ShopMarket_Web_API.Reprository.EmailReprository;
using ShopMarket_Web_API.Reprository.Interface;

namespace ShopMarket_Web_API.Reprository.repository
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
                var oldPassExist = await _userManager.CheckPasswordAsync(user, userPassDto.OldPassword);
                if (oldPassExist && userPassDto.NewPassword == userPassDto.ConfirmPassword)
                {
                    await _userManager.ChangePasswordAsync(user, userPassDto.OldPassword, userPassDto.NewPassword);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        public async Task<string> ConfirmEmail(int userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                return "User not found";

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
                return "Thank you for confirming your email";

            return "Email not confirmed";
        }

        public async Task<UserGetDto> CreateUser(SignUpUserDto userModel)
        {
            var userExists = await _userManager.FindByEmailAsync(userModel.Email);

            if (userExists is not null)
                throw new ArgumentException("Email is taken");

            var user = _mapper.Map<User>(userModel);
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _emailService.SendEmailAsync(userModel.Email, "Confirm Your Email", $"Please confirm your account YOur token : {emailConfirmationToken}", true);
                return _mapper.Map<UserGetDto>(user);
            }

            throw new ArgumentException(result.ToString());
        }

        public async Task<bool> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user is not null)
            {
                var result = await _userManager.SetLockoutEnabledAsync(user, true);
                if (result.Succeeded)
                    return true;
            }
            return false;
        }

        public async Task ForgetPasswordAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is not null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (token is null)
                    throw new ArgumentException("token not created");

                await _emailService.SendEmailAsync(Email, "reset Your Password", $"Please reset Your Password YOur token : {token}", true);
                user.ResetPasswordToken = token;
                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Email is not correct");
            }
        }

        public async Task<IList<UserGetDto>> GetActiveUsersAsync() =>
             _mapper.Map<List<UserGetDto>>(await _userManager.Users.Where(m => m.LockoutEnabled == false).ToListAsync());

        public async Task<IList<UserGetDto>> GetInActiveUsersAsync() =>
            _mapper.Map<List<UserGetDto>>(await _userManager.Users.Where(m => m.LockoutEnabled == true).ToListAsync());

        public async Task<bool> ResetPasswordAsync(int UserId, ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByIdAsync(UserId.ToString());
            if (user is not null)
            {
                if (user.ResetPasswordToken == resetPasswordDto.Token)
                {
                    await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
                    await _context.SaveChangesAsync();
                    return true;
                }
                throw new ArgumentException("Token not valid");
            }
            return false;
        }

        public async Task<UserGetDto> UpdateUser(int UserId, UpdateUserDto userDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == UserId);

            if (user is null)
                return null;

            _mapper.Map(userDto, user);
            var userGetDto = _mapper.Map<UserGetDto>(user);

            await _userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();
            return userGetDto;
        }
    }
}
