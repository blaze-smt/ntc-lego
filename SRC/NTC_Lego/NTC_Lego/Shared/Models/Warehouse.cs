using System.ComponentModel.DataAnnotations;

namespace NTC_Lego.Shared
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }

        [Required]
        [MaxLength(50)]
        public string WarehouseName { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}
