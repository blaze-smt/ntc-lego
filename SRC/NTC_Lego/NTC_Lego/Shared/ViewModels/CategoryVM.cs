using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class CategoryVM
    {
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public ICollection<Item>? Items { get; set; }
    }
}
