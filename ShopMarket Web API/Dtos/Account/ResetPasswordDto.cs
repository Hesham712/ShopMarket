using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShopMarket_Web_API.Dtos.Account
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
