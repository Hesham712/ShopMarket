using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Dtos.Order
{
    public class OrderItemsRequestDto
    {
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
        //public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}
