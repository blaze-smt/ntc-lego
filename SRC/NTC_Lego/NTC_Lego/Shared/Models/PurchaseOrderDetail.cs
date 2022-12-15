using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class PurchaseOrderDetail
    {
        [Key]
        [Required]
        public int PurchaseOrderDetailId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int PurchaseOrderDetailQuantity { get; set; }

        public int PurchaseOrderId { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; } = null!;

        public int InventoryId { get; set; }

        public Inventory Inventory { get; set; } = null!;

        [NotMapped]
        public decimal PurchaseOrderDetailTotalPrice { get { return this.Inventory.InventoryItemPrice * PurchaseOrderDetailQuantity; } }
    }
}
