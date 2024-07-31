using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Dtos.Account
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Name Field is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email Field is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone Number Field is required")]
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}
