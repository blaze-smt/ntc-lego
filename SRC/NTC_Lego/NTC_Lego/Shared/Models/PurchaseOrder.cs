using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public decimal PurchaseOrderTotalPrice { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [NotMapped]
        public ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        [NotMapped]
        public decimal SaleOrderTotalPrice { get { return PurchaseOrderDetails.Sum(x => x.PurchaseOrderDetailTotalPrice); } }
    }
}
