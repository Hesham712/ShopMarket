namespace ShopMarket_Web_API.Models
{
    public class Refund
    {

        public int Id { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public int OrderId { get; set; }
        public int ShiftId { get; set; }
        public Order? Order { get; set; }
        public Shift? Shift { get; set; }
        public IList<RefundItem>? RefundItems { get; set; }
    }
}
