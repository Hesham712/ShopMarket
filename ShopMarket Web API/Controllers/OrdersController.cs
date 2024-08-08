using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMarket_Web_API.Data;
using ShopMarket_Web_API.Dtos.Order;
using ShopMarket_Web_API.Models;
using ShopMarket_Web_API.Reprository.OrderReprository;

namespace ShopMarket_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpPost("{ShiftId}")]
        public async Task<IActionResult> CreateOrder([FromBody] List<OrderItemsRequestDto> orderDto, [FromRoute] int ShiftId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var order = await _orderRepository.CreateOrderAsync(orderDto, ShiftId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound($"{ex.Message}");
            }
        }

        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetOrderId()
        {
            try
            {
                var orders = await _orderRepository.GetOrderAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
