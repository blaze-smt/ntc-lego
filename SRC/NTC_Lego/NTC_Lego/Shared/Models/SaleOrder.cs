using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class SaleOrder
    {
        public int SaleOrderId { get; set; }
        public DateTime SaleOrderDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [NotMapped]
        public ICollection<SaleOrderDetail> SaleOrderDetails { get; set; }
        [NotMapped]
        public decimal SaleOrderTotalPrice { get { return SaleOrderDetails.Sum(x => x.SaleOrderDetailTotalPrice); } }
    }
}
