namespace NTC_Lego.Shared
{
    public class SaleOrder
    {
        public int SaleOrderId { get; set; }
        public DateTime SaleOrderDate { get; set; }
        public decimal SaleOrderTotalPrice { get; set; }
        // FK Customer
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        // public List<SaleOrderDetail>? SaleOrderDetails { get; set; }
    }
}
