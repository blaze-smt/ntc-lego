namespace NTC_Lego.Shared
{
    public class PurchaseOrderDetail
    {
        public int PurchaseOrderDetailId { get; set; }
        public int PurchaseOrderDetailQuantity { get; set; }
        public decimal PurchaseOrderSubTotalPrice { get; set; }
        // FK PurchaseOrder
        public int PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        // FK Inventory
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; }
    }
}
