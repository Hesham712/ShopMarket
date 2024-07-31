using ShopMarket_Web_API.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Dtos.Product
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        [Required]
        [Length(2,50)]
        public string? Name { get; set; }
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        public decimal DiscountPercentage { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;
        public IList<OrderItem>? OrderItems { get; set; }
    }
}
