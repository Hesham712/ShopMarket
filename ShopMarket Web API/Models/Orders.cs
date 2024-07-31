namespace ShopMarket_Web_API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public int ShiftId { get; set; }
        public Shift? Shift { get; set; }
        public IList<OrderItem>? OrderItems { get; set; }
        public IList<Refund>? Refunds { get; set; }

    }
}
