namespace NTC_Lego.Shared
{
    public class SaleOrder
    {
        public int SaleOrderId { get; set; }
        public DateTime SaleOrderDate { get; set; }
        public decimal SaleOrderTotalPrice { get; set; }
        // FK Customer
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
