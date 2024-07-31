using ShopMarket_Web_API.Dtos.Order;
using ShopMarket_Web_API.Dtos.Product;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Data.Interface
{
    public interface IOrderRepository
    {
        Task<List<OrderItemsDetailDto>> CreateOrderAsync(List<OrderItemsRequestDto> orderItemsDto, int shiftId);
        Task<IList<Order>> GetOrderAsync();
        Task<OrderByIdDto> GetOrderByIdAsync(int orderId);

    }
}
