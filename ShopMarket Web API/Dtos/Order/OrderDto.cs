using ShopMarket_Web_API.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Dtos.Order
{
    public class OrderByIdDto
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        [Range(0,100000)]
        public decimal TotalPrice { get; set; }
        [Required]
        public int ShiftId { get; set; }
        public IList<OrderItem>? OrderItems { get; set; }

    }
}
