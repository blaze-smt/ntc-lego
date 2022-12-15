using System.ComponentModel.DataAnnotations;

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
