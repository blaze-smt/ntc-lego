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


        [NotMapped]
        public ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
