using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class SaleOrder
    {
        [Key]
        [Required]
        public int SaleOrderId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SaleOrderDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [NotMapped]
        public ICollection<SaleOrderDetail> SaleOrderDetails { get; set; }

        [NotMapped]
        public decimal SaleOrderTotalPrice { get { return SaleOrderDetails.Sum(x => x.SaleOrderDetailTotalPrice); } }
    }
}
