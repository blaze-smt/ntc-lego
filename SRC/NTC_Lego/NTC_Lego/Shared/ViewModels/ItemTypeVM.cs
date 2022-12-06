using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class ItemTypeVM
    {
        public string? ItemTypeId { get; set; } = null!;
        public string? ItemTypeName { get; set; } = null!;
        public ICollection<Item>? Items { get; set; } = null!;
    }
}
