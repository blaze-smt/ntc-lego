namespace NTC_Lego.Shared
{
    public class SaleOrderDetail
    {
        public int SaleOrderDetailId { get; set; }
        public int SaleOrderDetailQuantity { get; set; }
        public decimal SaleOrderSubTotalPrice { get; set; }
        // FK SaleOrder
        public int SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; }
        // FK Inventory
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
