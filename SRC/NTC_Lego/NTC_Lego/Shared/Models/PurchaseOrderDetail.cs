using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public PurchaseOrder PurchaseOrder { get; set; }

        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        [NotMapped]
        public decimal PurchaseOrderDetailTotalPrice { get { return this.Inventory.InventoryItemPrice * PurchaseOrderDetailQuantity; } }
    }
}
