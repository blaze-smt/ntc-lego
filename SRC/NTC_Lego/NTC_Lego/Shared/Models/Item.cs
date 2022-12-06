using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class Item
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ItemId { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; } = null!;

        [Column(TypeName = "float")]
        public double? ItemWeight { get; set; }

        public string ItemTypeId { get; set; } = null!;

        public ItemType ItemType { get; set; } = null!;

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        [NotMapped]
        public ICollection<Inventory> Inventories { get; set; } = null!;

        [NotMapped]
        public string Image { get; set; } = null!;

        [NotMapped]
        public string BrickLinkURL { get; set; } = null!;
    }
}
