using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class InventoryVM
    {
        public int InventoryId { get; set; }
        public decimal InventoryItemPrice { get; set; }
        public string ColorName { get; set; }
        public string ItemId { get; set; }
        public int QuantityTotal { get { return this.InventoryLocations.Sum(x => x.ItemQuantity); } }
        public ICollection<InventoryLocationVM> InventoryLocations { get; set; }
        public ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; } = null!;
        public ICollection<SaleOrderDetail> SaleOrderDetails { get; set; } = null!;
    }
}
