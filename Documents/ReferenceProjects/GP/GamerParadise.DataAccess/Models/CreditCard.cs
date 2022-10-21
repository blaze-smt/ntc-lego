using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    public class CreditCard
    {
        [Key]
        [Required]
        public int CardId { get; set; }

        [Required(ErrorMessage = "You must include a security code.")]
        [MinLength(3, ErrorMessage = "The security code must be at least 3 characters.")]
        [MaxLength(4, ErrorMessage = "The security code cannot be greater than 4 characters.")]
        [RegularExpression("^[0-9]+$")]
        [Column(TypeName = "nvarchar(4)")]
        public string CSC { get; set; }

        [Required(ErrorMessage = "You must include your name as it appears on the card.")]
        [MaxLength(255, ErrorMessage = "Name on card cannot be more than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string NameOnCard { get; set; }

        [Required(ErrorMessage = "You must include a card number.")]
        [MaxLength(255, ErrorMessage = "Card number cannot be more than 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string CardNum { get; set; }

        [Required(ErrorMessage = "You must include the expiration month.")]
        public int ExpirationMonth { get; set; }

        [Required(ErrorMessage = "You must include the expiration year.")]
        public int ExpirationYear { get; set; }

        [Required(ErrorMessage = "You must include your billing address.")]
        [MaxLength(255, ErrorMessage = "Billing address cannot exceed 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        [Display(Name = "Billing Address")]
        public string Address { get; set; }

        [MaxLength(255, ErrorMessage = "Billing address line 2 cannot exceed 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        [Display(Name = "Apt or Suite Number")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "You must include your billing city.")]
        [MaxLength(255, ErrorMessage = "Billing address line 2 cannot exceed 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string City { get; set; }

        [Required(ErrorMessage = "You must include your billing state.")]
        [MaxLength(255, ErrorMessage = "Billing address line 2 cannot exceed 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string State { get; set; }

        [Required(ErrorMessage = "You must include your billing zip.")]
        [MaxLength(255, ErrorMessage = "Billing address line 2 cannot exceed 255 characters.")]
        [Column(TypeName = "nvarchar(255)")]
        public string Zip { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public List<Subscription> Subscriptions { get; set; }

        [Required]
        public bool IsArchived { get; set; }
    }
}