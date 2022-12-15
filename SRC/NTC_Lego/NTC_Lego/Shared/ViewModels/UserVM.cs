using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class UserVM
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public ICollection<SaleOrder> SaleOrders { get; set; } = null!;
    }
}
