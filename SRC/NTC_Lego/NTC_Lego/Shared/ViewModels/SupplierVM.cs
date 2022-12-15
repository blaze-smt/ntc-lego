using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class SupplierVM
    {
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierEmail { get; set; }
        public ICollection<PurchaseOrder>? PurchaseOrders { get; set; }
    }
}
