using System.ComponentModel.DataAnnotations;

namespace Finance_WebApi.Dtos.Account
{
    public class SignUpUserDto
    {
        [Required(ErrorMessage ="Name Field is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "UserName Field is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "UserName length must be between 5 and 20 characters")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email Field is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password Field is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
