using System.ComponentModel.DataAnnotations;

namespace NTC_Lego.Shared
{
    public class Supplier
    {
        [Key]
        [Required]
        public int SupplierId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SupplierName { get; set; }

        [MaxLength(100)]
        public string? SupplierEmail { get; set; }
    }
}
