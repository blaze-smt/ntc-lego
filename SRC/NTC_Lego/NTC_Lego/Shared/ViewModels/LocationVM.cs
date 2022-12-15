using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class LocationVM
    {
        public int? LocationId { get; set; }
        public string? BinName { get; set; }
        public int? WarehouseId { get; set; }
        public WarehouseVM? Warehouse { get; set; }
        public ICollection<InventoryLocationVM>? InventoryLocations { get; set; }
    }
}
