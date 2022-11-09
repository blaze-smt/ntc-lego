using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    public class SaleOrder
    {
        [Key]
        [Required]
        public int SaleOrderId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SaleOrderDate { get; set; }

        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.Unshipped;

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [NotMapped]
        public ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = null!;

        [NotMapped]
        public decimal SaleOrderTotalPrice { get { return SaleOrderDetails.Sum(x => x.SaleOrderDetailTotalPrice); } }
    }
}
