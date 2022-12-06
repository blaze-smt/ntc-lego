using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class WarehouseVM
    {
        public int? WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public ICollection<LocationVM>? Locations { get; set; }
    }
}
