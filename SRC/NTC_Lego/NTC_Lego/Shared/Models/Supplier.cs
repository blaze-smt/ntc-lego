using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    public class Supplier
    {
        [Key]
        [Required]
        public int SupplierId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SupplierName { get; set; } = null!;

        [MaxLength(100)]
        public string? SupplierEmail { get; set; }

        [JsonIgnore]
        [NotMapped]
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = null!;
    }
}
