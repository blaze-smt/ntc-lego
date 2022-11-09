using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class PurchaseOrderDetailVM
    {
        public int PurchaseOrderDetailId { get; set; }
        public int PurchaseOrderDetailQuantity { get; set; }
        public decimal InventoryItemPrice { get; set; }
        public decimal PurchaseOrderDetailTotalPrice { get { return InventoryItemPrice * PurchaseOrderDetailQuantity; } }
    }
}
