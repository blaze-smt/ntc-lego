using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class SaleOrderDetailVM
    {
        public int SaleOrderDetailId { get; set; }
        public int SaleOrderDetailQuantity { get; set; }
        public decimal InventoryItemPrice { get; set; }
        public decimal SaleOrderDetailTotalPrice { get { return InventoryItemPrice * SaleOrderDetailQuantity; } }
    }
}
