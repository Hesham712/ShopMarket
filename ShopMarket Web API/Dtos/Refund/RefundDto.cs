using ShopMarket_Web_API.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Dtos.Refund
{
    public class RefundDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderId { get; set; }
        public int ShiftId { get; set; }
    }
}
