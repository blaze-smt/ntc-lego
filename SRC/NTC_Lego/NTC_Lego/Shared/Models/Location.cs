using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class Location
    {
        [Key]
        [Required]
        public int LocationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string BinName { get; set; } = null!;

        public int WarehouseId { get; set; }

        public Warehouse Warehouse { get; set; } = null!;

        [NotMapped]
        public ICollection<InventoryLocation> InventoryLocations { get; set; }
    }
}
