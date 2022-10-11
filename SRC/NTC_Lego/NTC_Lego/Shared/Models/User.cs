using System.ComponentModel.DataAnnotations;

namespace NTC_Lego.Shared
{
    public class User
    {
        
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; }
    }
}
