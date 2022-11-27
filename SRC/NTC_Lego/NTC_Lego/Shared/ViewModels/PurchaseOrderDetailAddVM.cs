using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class PurchaseOrderDetailAddVM
    {
        public string ItemId { get; set; } = "10048";
        public int ColorId { get; set; } = 0;
        public int LocationId { get; set; } = 1;
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; } = 0.58m;

        //public decimal PurchaseOrderDetailTotalPrice { get { return this.Inventory.InventoryItemPrice * PurchaseOrderDetailQuantity; } }
    }
}
