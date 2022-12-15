using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class InventoryLocationVM
    {
        public int? InventoryId { get; set; }
        public InventoryVM? Inventory { get; set; }
        public int? LocationId { get; set; }
        public LocationVM? Location { get; set; }
        public int ItemQuantity { get; set; } = 0;
    }
}
