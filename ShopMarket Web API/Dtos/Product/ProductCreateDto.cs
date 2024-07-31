using ShopMarket_Web_API.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Dtos.Product
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage = "The ProductName is required.")]
        [StringLength(100,MinimumLength =2, ErrorMessage = "ProductName must be between 2 and 100 characters")]
        public string? Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The ProductStock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please sure productStock range greater than 0")]
        public int? Stock { get; set; }

        [Required(ErrorMessage = "The ProductPrice is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please sure productPrice range between 1 and 1000")]
        public decimal? Price { get; set; }
        public decimal? DiscountPercentage { get; set; } = decimal.Zero;
    }
}
