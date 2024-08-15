using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMarket_Web_API.Dtos.Order;
using ShopMarket_Web_API.Dtos.Refund;
using ShopMarket_Web_API.Reprository.RefundReprository;
using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundsController : ControllerBase
    {
        private readonly IRefundRepository _refundRepository;

        public RefundsController(IRefundRepository refundRepository)
        {
            _refundRepository = refundRepository;
        }

        [HttpPost("{ShiftId},{OrderId}")]
        public async Task<IActionResult> CreateRefund([FromBody] List<RefundItemsRequestDto> refundDto,[FromRoute] int ShiftId,[FromRoute] int OrderId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var refund = await _refundRepository.CreateRefundAsync(refundDto, ShiftId, OrderId);
                return Ok(refund);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message.ToString()});
            }
        }

        [HttpGet("GetAllRefund")]
        public async Task<IActionResult> GetRefundAsync()
        {
            return Ok(await _refundRepository.GetRefundAsync());
        }

        [HttpGet("{RefundId}")]
        public async Task<IActionResult> GetRefundByIdAsync([FromRoute] int RefundId)
        {
            return Ok(await _refundRepository.GetRefundByIdAsync(RefundId));
        }
    }
}
