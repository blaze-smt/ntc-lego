﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class ColorVM
    {
        public int? ColorId { get; set; }
        public string? ColorName { get; set; }
        public string? ColorValue { get; set; }
        public string? ColorType { get; set; }
        public ICollection<InventoryVM>? Inventories { get; set; }
    }
}
