using System.ComponentModel.DataAnnotations;

namespace AdminCreateUserDemo.Models
{
    public sealed class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must have at least one uppercase letter and one special character.")]
        public string Password { get; set; }
    }
}
