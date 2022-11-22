﻿using System;
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
        public int? InventoryId { get; set; }
        public InventoryVM? Inventory { get; set; }
        public int? LocationId { get; set; }
        public LocationVM? Location { get; set; }
        public int ItemQuantity { get; set; } = 0;
    }
}