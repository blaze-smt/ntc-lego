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
        [Range(0.0, int.MaxValue)]
        public int InventoryQuantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,4)")]
        public decimal InventoryItemPrice { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }
        public string ItemId { get; set; }
        public Item Item { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
