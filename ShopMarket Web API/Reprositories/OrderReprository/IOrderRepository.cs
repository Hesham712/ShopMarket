using ShopMarket_Web_API.Dtos.Order;
using ShopMarket_Web_API.Dtos.Product;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Reprository.OrderReprository
{
    public interface IOrderRepository
    {
        Task<List<OrderItemsDetailDto>> CreateOrderAsync(List<OrderItemsRequestDto> orderItemsDto, int shiftId);
        Task<IList<OrderDto>> GetOrderAsync();
        Task<OrderByIdDto> GetOrderByIdAsync(int orderId);
        Task<bool> OrderExist(int orderId);

    }
}
