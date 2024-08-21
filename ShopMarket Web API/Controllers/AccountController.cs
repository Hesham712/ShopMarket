using AutoMapper;
using Finance_WebApi.Dtos.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using ShopMarket_Web_API.Data;
using ShopMarket_Web_API.Dtos.Account;
using ShopMarket_Web_API.Models;
using ShopMarket_Web_API.Reprository.EmailReprository;
using ShopMarket_Web_API.Reprository.Interface;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopMarket_Web_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager, IEmailService emailService, IUserRepository userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailService = emailService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginResult = await _userRepository.Login(loginDto);

            return Ok(loginResult);
        }

        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var SignUpResult = await _userRepository.SignUp(model);

            return Ok(SignUpResult);
        }

        [HttpGet("GetActiveUser")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetActiveUser()
        {
            var result = await _userRepository.GetActiveUsersAsync();
            if (result != null)
                return Ok(result);

            return BadRequest(ModelState.ToString());
        }

        [HttpGet("GetInActiveUser")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetInActiveUser()
        {
            var result = await _userRepository.GetInActiveUsersAsync();
            if (result != null)
                return Ok(result);

            return BadRequest(ModelState.ToString());
        }

        [HttpPost("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody][Required] int UserId, string token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());
            return Ok(await _userRepository.ConfirmEmail(UserId, token));
        }

        [HttpPut("{UserId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int UserId, UpdateUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());

            var result = await _userRepository.UpdateUser(UserId, userDto);
            if (result != null)
                return Ok(result);
            return BadRequest(ModelState.ToString());
        }

        [HttpPost("Delete")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete([FromBody] string UserName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());

            var user = await _userRepository.DeleteUser(UserName);
            if (user)
                return Ok($"UserName : {UserName} has been locked");
            else
                return BadRequest("User Not Found");
        }

        [HttpPost("ChangePassword{UserId}")]
        public async Task<IActionResult> ChangePassword([FromRoute] int UserId, [FromBody] UpdateUserPasswordDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());

            var PasswordUserChanged = await _userRepository.ChangePasswordAsync(UserId, userDto);
            if (PasswordUserChanged)
            {
                return StatusCode(200, "Password Changed Successfully");
            }
            else
            {
                return StatusCode(400, "Invalid password");
            }
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromQuery] string Email)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());
            try
            {
                await _userRepository.ForgetPasswordAsync(Email);

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message ?? ex.Message.ToString());
            }

        }

        [HttpPost("Reset Password")]
        public async Task<IActionResult> ResetPassword([FromQueryAttribute] int UserId,[FromBody]ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());
            try
            {
                await _userRepository.ResetPasswordAsync(UserId,resetPasswordDto);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message ?? ex.Message.ToString());
            }
        }
    }
}
