using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class ItemVM
    {
        public string? ItemId { get; set; }
        public string? ItemName { get; set; }
        public double? ItemWeight { get; set; }
        public string? ItemTypeId { get; set; }
        public ItemTypeVM? ItemType { get; set; } 
        public int? CategoryId { get; set; }
        public CategoryVM? Category { get; set; }
        public ICollection<InventoryVM>? Inventories { get; set; } 
        public string? Image { get; set; }
        public string? BrickLinkURL { get; set; } 
    }
}
