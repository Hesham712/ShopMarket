using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Dtos.Refund
{
    public class RefundDetailsDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public int OrderId { get; set; }
        public int ShiftId { get; set; }
        public IList<RefundItemsDetailDto> RefundItems { get; set; }
    }
}
