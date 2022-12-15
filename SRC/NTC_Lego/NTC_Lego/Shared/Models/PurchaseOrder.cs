using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class PurchaseOrder
    {
        [Key]
        [Required]
        public int PurchaseOrderId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PurchaseOrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; } = null!;

        [NotMapped]
        public ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = null!;

        [NotMapped]
        public decimal PurchaseOrderTotalPrice { get { return PurchaseOrderDetails.Sum(x => x.PurchaseOrderDetailTotalPrice); } }
    }
}
