using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class SaleOrderDetail
    {
        [Key]
        [Required]
        public int SaleOrderDetailId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SaleOrderDetailQuantity { get; set; }

        public int SaleOrderId { get; set; }

        public SaleOrder SaleOrder { get; set; } = null!;

        public int InventoryId { get; set; }

        public Inventory Inventory { get; set; } = null!;

        [NotMapped]
        public decimal SaleOrderDetailTotalPrice { get { return this.Inventory.InventoryItemPrice * SaleOrderDetailQuantity; } }
    }
}
