using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    public class Location
    {
        [Key]
        [Required]
        public int LocationId { get; set; }

        [Required]
        [MaxLength(50)]
        public string BinName { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ICollection<InventoryLocation> InventoryLocations { get; set; }
    }
}
