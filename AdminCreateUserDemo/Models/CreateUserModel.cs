using System.ComponentModel.DataAnnotations;

namespace AdminCreateUserDemo.Models
{
    public sealed class CreateUserModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
