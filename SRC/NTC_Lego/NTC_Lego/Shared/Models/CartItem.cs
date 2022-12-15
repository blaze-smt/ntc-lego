using System.ComponentModel.DataAnnotations;

namespace NTC_Lego.Shared
{
    public class CartItem
    {
        [Key]
        [Required]
        public int CartItemId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int CartItemQuantity { get; set; }

        public int InventoryId { get; set; }

        public Inventory? Inventory { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
