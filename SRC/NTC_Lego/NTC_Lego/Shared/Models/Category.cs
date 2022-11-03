using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        [MaxLength(50)]
        public string? CategoryName { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ICollection<Item> Items { get; set; }
    }
}
