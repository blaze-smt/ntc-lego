using System.ComponentModel.DataAnnotations;
using GamerParadise.DataAccess.Models;
using Microsoft.AspNetCore.Http;

namespace GamerParadise.Models
{
    public class EditProfileViewModel
    {
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".gif" })]
        [Display(Name = "Profile Image")]
        public IFormFile NewProfileImage { get; set; }

        [Display(Name = "First Name"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name"), Required]
        public string LastName { get; set; }

        [Display(Name = "Email Address"), Required]
        public string EmailAddress { get; set; }

        [Display(Name = "Username"), Required]
        public string Username { get; set; }

        [Required, Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Compare(nameof(ConfirmPassword)), Display(Name = "New password"), StringLength(255, ErrorMessage = "Your password must be between 5 and 255 characters.", MinimumLength = 5)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm your password.")]
        public string ConfirmPassword { get; set; }
    }
}