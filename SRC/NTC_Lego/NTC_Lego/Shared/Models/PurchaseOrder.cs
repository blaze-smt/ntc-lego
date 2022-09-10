namespace NTC_Lego.Shared
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public decimal PurchaseOrderTotalPrice { get; set; }
        // FK Supplier
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        // public List<PurchaseOrderDetail>? PurchaseOrderDetails { get; set; }
    }
}
