using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class ColorVM
    {
        public int? ColorId { get; set; }
        public string? ColorName { get; set; }
        public string? ColorValue { get; set; }
        public string? ColorType { get; set; }
        public ICollection<InventoryVM>? Inventories { get; set; }
    }
}
