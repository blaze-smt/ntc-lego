using System.ComponentModel.DataAnnotations;

namespace GamerParadise.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "You must include your email address to login.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "You must include your password to login.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}