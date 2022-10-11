using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    public class Color
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ColorId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ColorName { get; set; }

        public string? ColorValue { get; set; }

        [Required]
        [MaxLength(50)]
        public string ColorType { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
    }
}
