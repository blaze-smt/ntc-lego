using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerParadise.DataAccess.Models
{
    public class Subscription
    {
        [Key]
        [Required]
        public int SubscriptionId { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        [Required(ErrorMessage = "You must select a credit card.")]
        public int CardId { get; set; }

        public CreditCard Card { get; set; }

        [Required]
        public int PlanId { get; set; }

        public Plan Plan { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }

        public int Downloads { get; set; }
    }
}