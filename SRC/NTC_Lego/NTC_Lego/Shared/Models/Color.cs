﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    public class Color
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ColorId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ColorName { get; set; }

        [MaxLength(10)]
        public string? ColorValue { get; set; }

        [Required]
        [MaxLength(50)]
        public string ColorType { get; set; }

        [NotMapped]
        [JsonIgnore]
        public ICollection<Inventory> Inventories { get; set; }
    }
}
