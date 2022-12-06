using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class Inventory
    {
        [Key]
        [Required]
        public int InventoryId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal InventoryItemPrice { get; set; }

        public int ColorId { get; set; }

        public Color Color { get; set; } = null!;

        public string ItemId { get; set; } = null!;

        public Item Item { get; set; } = null!;

        [NotMapped]
        public int QuantityTotal { get { return InventoryLocations.Sum(x => x.ItemQuantity); } }

        [NotMapped]
        public virtual ICollection<InventoryLocation> InventoryLocations { get; set; }

        [NotMapped]
        public ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = null!;

        [NotMapped]
        public ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = null!;

        [NotMapped]
        public ICollection<CartItem>? CartItems { get; set; }
    }
}
