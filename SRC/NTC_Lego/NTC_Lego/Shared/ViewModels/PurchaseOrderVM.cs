using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class PurchaseOrderVM
    {
        public int PurchaseOrderId { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public int? SupplierId { get; set; }
        public SupplierVM? Supplier { get; set; }
        public ICollection<PurchaseOrderDetailVM>? PurchaseOrderDetails { get; set; }
        public decimal PurchaseOrderTotalPrice { get { return PurchaseOrderDetails.Sum(x => x.PurchaseOrderDetailTotalPrice); } }
    }
}
