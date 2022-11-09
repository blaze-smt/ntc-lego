using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class SaleOrderVM
    {
        public int SaleOrderId { get; set; }
        public DateTime? SaleOrderDate { get; set; }
        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Unshipped;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        public string UserName { get; set; }
        public ICollection<SaleOrderDetailVM> SaleOrderDetails { get; set; } = null!;
        public decimal SaleOrderTotalPrice { get { return SaleOrderDetails.Sum(x => x.SaleOrderDetailTotalPrice); } }
    }
}
