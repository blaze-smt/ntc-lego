using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class SaleOrderDetailVM
    {
        public int? SaleOrderDetailId { get; set; }
        public int SaleOrderDetailQuantity { get; set; }
        public int? SaleOrderId { get; set; }
        public SaleOrderVM? SaleOrder { get; set; }
        public int? InventoryId { get; set; }
        public InventoryVM? Inventory { get; set; }
        public decimal SaleOrderDetailTotalPrice { get { return this.Inventory.InventoryItemPrice * SaleOrderDetailQuantity; } }
    }
}
