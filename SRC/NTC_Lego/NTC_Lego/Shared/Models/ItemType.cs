using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    public class ItemType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(1)]
        public string ItemTypeId { get; set; }

        [Required]
        [MaxLength(20)]
        public string ItemTypeName { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ICollection<Item> Items { get; set; }
    }
}
