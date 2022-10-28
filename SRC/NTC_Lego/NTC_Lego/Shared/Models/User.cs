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
        public string UserName { get; set; }

        [MaxLength(100)]
        public string? UserEmail { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [NotMapped, Display(Name = "Full Name")]
        public string FullName { get => FirstName + ' ' + LastName; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public bool? IsAdmin { get; set; }

/*        [NotMapped]
        public List<CartItem> CartItems { get; set; }*/

        [Display(Name = "Mailing Address")]
        public string? Address { get; set; }

        [Display(Name = "Apt or Suite Number")]
        public string? Address2 { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Zip { get; set; }

        [NotMapped]
        public ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
