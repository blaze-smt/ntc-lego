using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NTC_Lego.Shared.ViewModels
{
    [NotMapped]
    public class LocationVM
    {
        public int LocationId { get; set; }
        public string BinName { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        //public ICollection<InventoryLocation> InventoryLocations { get; set; }
    }
}
