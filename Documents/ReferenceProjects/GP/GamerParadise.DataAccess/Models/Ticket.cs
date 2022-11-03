using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    public class Ticket
    {
        [Key]
        [Required]
        public int TicketId { get; set; }

        [Required(ErrorMessage = "You must include your name.")]
        [MaxLength(255, ErrorMessage = "The Name can not be more than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must include your email address.")]
        [MinLength(3, ErrorMessage = "The Email must be at least 3 characters.")]
        [MaxLength(255, ErrorMessage = "The Email cannot be more than 255 characters.")]
        [DataType(DataType.EmailAddress)]
        [Column(TypeName = "nvarchar(255)")]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please detail what you are enquiring about.")]
        [MinLength(1, ErrorMessage = "Please detail what you are enquiring about.")]
        [MaxLength(1000, ErrorMessage = "Enquiry must be less than 1000 characters.")]
        [Column(TypeName = "nvarchar(1000)")]
        [Display(Name = "Let us know what's on your mind")]
        public string Message { get; set; }

        [Required]
        public bool Responded { get; set; }
    }
}