using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMarket_Web_API.Dtos.Shift;
using ShopMarket_Web_API.Reprository.Interface;

namespace ShopMarket_Web_API.Controllers
{
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

    }
}
