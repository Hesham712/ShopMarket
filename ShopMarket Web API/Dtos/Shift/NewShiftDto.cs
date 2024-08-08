
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Dtos.Shift
{
    public class NewShiftDto
    {
        public Decimal TotalCash { get; set; } = Decimal.Zero;
        public int UserId { get; set; }
    }
}
