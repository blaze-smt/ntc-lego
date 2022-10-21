using System.ComponentModel.DataAnnotations;

namespace GamerParadise.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "You must include your first name.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must include your last name.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must include your email address.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Your username must be at least 5 characters.")]
        [MaxLength(255, ErrorMessage = "Your username cannot be more than 255 characters.")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Your password must be between 5 and 255 characters.", MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm your Password")]
        public string PasswordConfirm { get; set; }
    }
}