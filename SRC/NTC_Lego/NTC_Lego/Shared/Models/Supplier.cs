using System.ComponentModel.DataAnnotations;

namespace NTC_Lego.Shared
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SupplierName { get; set; }
    }
}
