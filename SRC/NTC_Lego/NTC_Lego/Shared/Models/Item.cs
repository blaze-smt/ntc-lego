using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal ItemWeight { get; set; }
        public string ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Inventory> Inventories { get; set; }

        [NotMapped]
        public ICollection<Color> Colors { get; set; }
    }
}
