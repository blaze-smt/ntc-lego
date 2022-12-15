using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Your username must be 50 characters or less.")]
        public string UserName { get; set; } = null!;

        [Required, StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage = "Your passwords do not match.")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
