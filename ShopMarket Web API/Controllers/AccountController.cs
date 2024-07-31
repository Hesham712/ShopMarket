using AutoMapper;
using Finance_WebApi.Dtos.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopMarket_Web_API.Data.Interface;
using ShopMarket_Web_API.Dtos.Account;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Controllers
{
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
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.Users.FirstOrDefaultAsync(m => m.UserName == loginDto.UserName.ToLower());

            if (user == null) 
                return Unauthorized("Invalid UserName!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) 
                return Unauthorized("Invalid username and/or Password !");
            var resultData = _mapper.Map<LoginDataDto>(user);
            resultData.Token = _tokenService.CreateToken(user);
            return Ok(resultData);
            
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);
        //        var searchUser = await _userManager.FindByNameAsync(registerDto.UserName);
        //        if(searchUser != null)
        //            return BadRequest("this UserName Already exist");

        //        var User = new User
        //        {
        //            Name = registerDto.Name,
        //            UserName = registerDto.UserName,
        //            PasswordHash = registerDto.Password,
        //            Email = registerDto.Email
        //        };

        //        var createdUser = await _userManager.CreateAsync(User,registerDto.Password);
        //        if (createdUser.Succeeded)
        //        {
        //            return Ok(new NewUserDto
        //            {
        //                UserName = User.UserName,
        //                Email = User.Email,
        //                Token = _tokenService.CreateToken(User)
        //            });
        //        }
        //        else
        //        {
        //            return StatusCode(500, createdUser.Errors.ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpUserDto model)
        {
            try
            {
                var user = await _userRepository.CreateUser(model);
                if (user != null)
                {
                    var resultData = _mapper.Map<LoginDataDto>(user);
                    resultData.Token = _tokenService.CreateToken(user);
                    return Ok(resultData);
                }
                return BadRequest("not created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message ?? ex.Message.ToString());
            }
        }

        [HttpGet("GetActiveUser")]
        public async Task<IActionResult> GetActiveUser()
        {
            var result = await _userRepository.GetActiveUserAsync();
            if (result != null)
                return Ok(result);

            return BadRequest(ModelState.ToString());
        }

        [HttpGet("GetInActiveUser")]
        public async Task<IActionResult> GetInActiveUser()
        {
            var result = await _userRepository.GetInActiveUserAsync();
            if (result != null)
                return Ok(result);

            return BadRequest(ModelState.ToString());
        }

        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] int UserId, string token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());
            return Ok(await _userRepository.ConfirmEmail(UserId, token));
        }

        [HttpPut("{UserId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int UserId,UpdateUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ToString());

            var result = await _userRepository.UpdateUser(UserId, userDto);
            if (result != null) 
                return Ok(result);
            return BadRequest(ModelState.ToString());
        }

        [HttpPost("Delete")]
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
    }
}
