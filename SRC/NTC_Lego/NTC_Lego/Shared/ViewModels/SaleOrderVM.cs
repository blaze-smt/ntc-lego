using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class SaleOrderVM
    {
        public int? SaleOrderId { get; set; }
        public DateTime SaleOrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public int? UserId { get; set; }
        public UserVM? User { get; set; }
        public ICollection<SaleOrderDetailVM>? SaleOrderDetails { get; set; }
        public decimal SaleOrderTotalPrice { get { return SaleOrderDetails.Sum(x => x.SaleOrderDetailTotalPrice); } }
    }
}
