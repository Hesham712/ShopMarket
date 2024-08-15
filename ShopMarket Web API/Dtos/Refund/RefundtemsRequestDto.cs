using ShopMarket_Web_API.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Dtos.Refund    
{
    public class RefundItemsRequestDto
    {
        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Quantity must be greater than zero")]
        public int Quantity { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero")]
        public int OrderItemId { get; set; }
    }
}
