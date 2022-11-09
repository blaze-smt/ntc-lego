using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class PurchaseOrderVM
    {
        public int PurchaseOrderId { get; set; }
        public DateTime? PurchaseOrderDate { get; set; }
        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Unshipped;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        public string SupplierName { get; set; }
        public ICollection<PurchaseOrderDetailVM> PurchaseOrderDetails { get; set; } = null!;
        public decimal PurchaseOrderTotalPrice { get { return PurchaseOrderDetails.Sum(x => x.PurchaseOrderDetailTotalPrice); } }
    }
}
