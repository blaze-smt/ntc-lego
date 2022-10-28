using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class Item
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ItemId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ItemName { get; set; }

        [Column(TypeName = "float")]
        public double? ItemWeight { get; set; }

        public string ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [NotMapped]
        public ICollection<Inventory> Inventories { get; set; }

        [NotMapped]
        public string Image { get; set; }

        [NotMapped]
        public string BrickLinkURL { get; set; }
    }
}
