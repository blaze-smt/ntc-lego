using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NTC_Lego.Shared.ViewModels
{
    [NotMapped]
    public class InventoryVM
    {
        public int InventoryId { get; set; }

        public decimal InventoryItemPrice { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }
        public string ItemId { get; set; }
        public Item Item { get; set; }

        public int QuantityTotal { get { return InventoryLocations.Sum(x => x.ItemQuantity); } }

        public ICollection<InventoryLocationVM> InventoryLocations { get; set; }
    }
}
