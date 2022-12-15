using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }

        [MaxLength(50)]
        public string? CategoryName { get; set; }

        [NotMapped]
        public ICollection<Item> Items { get; set; } = null!;
    }
}
