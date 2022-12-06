using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class Warehouse
    {
        [Key]
        [Required]
        public int WarehouseId { get; set; }

        [Required]
        [MaxLength(50)]
        public string WarehouseName { get; set; } = null!;

        [NotMapped]
        public ICollection<Location> Locations { get; set; } = null!;
    }
}
