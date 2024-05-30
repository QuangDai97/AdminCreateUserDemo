using System.ComponentModel.DataAnnotations;

namespace AdminCreateUserDemo.Models
{
    public sealed class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must have at least one uppercase letter and one special character.")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$", ErrorMessage = "Password must have at least one uppercase letter and one special character.")]
        public string NewPassword { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }
}
