using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMarket_Web_API.Dtos.Shift;
using ShopMarket_Web_API.Reprository.Interface;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Linq;
using ShopMarket_Web_API.Helper;

namespace ShopMarket_Web_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftsController : ControllerBase
    {
        private readonly IShiftRepository _shiftRepository;

        public ShiftsController(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        [HttpPost("OpenShift")]
        public async Task<IActionResult> OpenShift([FromBody] NewShiftDto shiftDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await _shiftRepository.OpenShift(shiftDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message ?? ex.Message.ToString());
            }
        }

        [HttpPost("CloseShift")]
        public async Task<IActionResult> CloseShift()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var UserName = await DecodeJWT.GetUserNameFromToken(HttpContext);
            await _shiftRepository.CloseShift(UserName);

            return Ok();
        }
    }
}
