using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    public class Inventory
    {
        [Key]
        [Required]
        public int InventoryId { get; set; }

        [Required]
        [Range(0.0, int.MaxValue)]
        public int InventoryQuantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,4)")]
        public decimal InventoryItemPrice { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; } = null!;
        public string ItemId { get; set; } = null!;
        public Item Item { get; set; } = null!;

        public int LocationId { get; set; }
        public Location Location { get; set; } = null!;

        [JsonIgnore]
        [NotMapped]
        public ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = null!;

        [JsonIgnore]
        [NotMapped]
        public ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = null!;
    }
}
