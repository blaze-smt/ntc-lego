using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class SaleOrderDetail
    {
        public int SaleOrderDetailId { get; set; }
        public int SaleOrderDetailQuantity { get; set; }
        public int SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        [NotMapped]
        public decimal SaleOrderDetailTotalPrice { get { return this.Inventory.InventoryItemPrice * SaleOrderDetailQuantity; } }
    }
}
