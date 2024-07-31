namespace ShopMarket_Web_API.Models
{
    public class RefundItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderItemId { get; set; }
        public int RefundId { get; set; }
        public OrderItem? OrderItem { get; set; }
        public Refund? Refund { get; set; }
    }
}
