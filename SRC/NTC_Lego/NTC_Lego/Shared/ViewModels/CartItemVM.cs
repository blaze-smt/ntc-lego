using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NTC_Lego.Shared
{
    public class CartItemVM
    {
        public int CartItemId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int CartItemQuantity { get; set; }
        public int InventoryId { get; set; }
        public InventoryVM? Inventory { get; set; }
        public int UserId { get; set; }
        public UserVM? User { get; set; }
    }
}
