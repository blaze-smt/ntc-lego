using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class PurchaseOrderDetailVM
    {
        public int? PurchaseOrderDetailId { get; set; }
        public int PurchaseOrderDetailQuantity { get; set; }
        public int? PurchaseOrderId { get; set; }
        public PurchaseOrderVM? PurchaseOrder { get; set; }
        public int? InventoryId { get; set; }
        public InventoryVM? Inventory { get; set; }
        public decimal PurchaseOrderDetailTotalPrice { get { return this.Inventory.InventoryItemPrice * PurchaseOrderDetailQuantity; } }
    }
}
