using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class ItemType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ItemTypeId { get; set; }
        public string ItemTypeName { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
