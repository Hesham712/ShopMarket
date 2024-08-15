using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Dtos.Refund
{
    public class RefundItemsDetailDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
        public int OrderItemId { get; set; }
        public int RefundId { get; set; }
    }
}
