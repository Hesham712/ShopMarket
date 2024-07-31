using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Dtos.Account
{
    public class UpdateUserPasswordDto
    {
        [Required(ErrorMessage = "Old Password Field is required")]
        public string? OldPassword { get; set; }

        [Required(ErrorMessage = "New Password Field is required")]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password Field is required")]
        public string? ConfirmPassword { get; set; }
    }
}
