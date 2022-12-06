using System.ComponentModel.DataAnnotations;

namespace NTC_Lego.Shared
{
    public class InventoryLocation
    {
        [Required]
        public int InventoryId { get; set; }

        public Inventory Inventory { get; set; }

        [Required]
        public int LocationId { get; set; }

        public Location Location { get; set; }

        [Range(0.0, int.MaxValue)]
        public int ItemQuantity { get; set; } = 0;
    }
}
