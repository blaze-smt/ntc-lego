using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class User
    {
        [Key]
        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string UserEmail { get; set; } = null!;

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(100)]
        public string? PasswordHash { get; set; }

        [Required]
        public bool IsAdmin { get; set; } = false;

        [MaxLength(100)]
        [Display(Name = "Mailing Address")]
        public string? Address { get; set; }

        [MaxLength(100)]
        [Display(Name = "Apt or Suite Number")]
        public string? Address2 { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(2)]
        public string? State { get; set; }

        [MaxLength(10)]
        public string? Zip { get; set; }

        [NotMapped]
        public ICollection<CartItem>? CartItems { get; set; }

        [NotMapped, Display(Name = "Full Name")]
        public string? FullName { get => FirstName + ' ' + LastName; }

        [NotMapped]
        public ICollection<SaleOrder> SaleOrders { get; set; } = null!;
    }
}
