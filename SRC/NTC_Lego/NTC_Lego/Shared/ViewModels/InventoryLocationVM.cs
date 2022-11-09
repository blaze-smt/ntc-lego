using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class InventoryLocationVM
    {
        public int ItemQuantity { get; set; } = 0;
        public string BinName { get; set; }
        public string WarehouseName { get; set; }
    }
}
