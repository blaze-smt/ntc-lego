using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "You must include your first name.")]
        [MinLength(1, ErrorMessage = "Your first name must be at least 1 character.")]
        [MaxLength(255, ErrorMessage = "Your first name cannot be more than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "You must include your last name.")]
        [MinLength(1, ErrorMessage = "Your last name must be at least 1 character.")]
        [MaxLength(255, ErrorMessage = "Your last name cannot be more than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "You must include your email address.")]
        [MinLength(3, ErrorMessage = "Your email address must be at least 3 characters.")]
        [MaxLength(255, ErrorMessage = "Your email address cannot be more than 255 characters.")]
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "nvarchar(255)")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "You must include your display username.")]
        [MinLength(5, ErrorMessage = "Your username must be at least 5 characters.")]
        [MaxLength(255, ErrorMessage = "Your username cannot be more than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string Username { get; set; }

        [Required(ErrorMessage = "You must include a password.")]
        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "Your password must be between 5 and 255 characters", MinimumLength = 5)]
        public string PasswordHash { get; set; }

        [Display(Name = "Profile Image")]
        public byte[] ProfileImage { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsAdmin { get; set; }

        public List<CreditCard> Cards { get; set; }

        public List<GameLibrary> GameLibraries { get; set; }

        public List<Review> Reviews { get; set; }

#nullable enable
        public Subscription? Subscription { get; set; }
#nullable disable

        [Required]
        public bool IsArchived { get; set; }

        [NotMapped]
        public string Base64ProfileImageString
        {
            get
            {
                if (ProfileImage != null)
                {
                    return "data:image/png;base64," + Convert.ToBase64String(ProfileImage, 0, ProfileImage.Length);
                }
                else { return "/images/DefaultProfileImage.png"; }
            }
        }
    }
}