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
    public class InventoryLocation
    {
        [Required]
        public int InventoryId { get; set; }

        public Inventory Inventory { get; set; }

        [Required]
        public int LocationId { get; set; }

        public Location Location { get; set; }

        [Range(0.0, int.MaxValue)]
        public int ItemQuantity { get; set; } = 0;
    }
}
