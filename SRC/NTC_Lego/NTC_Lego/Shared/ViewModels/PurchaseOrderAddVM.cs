using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class PurchaseOrderAddVM
    {
        public DateTime PurchaseOrderDate { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public int SupplierId { get; set; } = 1;
        public string SupplierName { get; set; } = "Super Toy Inc.";
        public ICollection<PurchaseOrderDetailAddVM>? PurchaseOrderDetails { get; set; }

        //public decimal PurchaseOrderTotalPrice { get { return PurchaseOrderDetails.Sum(x => x.PurchaseOrderDetailTotalPrice); } }
    }
}
