using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC_Lego.Shared
{
    [NotMapped]
    public class InventoryAddVM
    {
        [Required(ErrorMessage = "Please enter a valid price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value between {1} and {2}.")]
        [DataType(DataType.Currency)]
        public decimal InventoryItemPrice { get; set; } = 1.50m;

        [Required(ErrorMessage = "Please select an item color.")]
        public int ColorId { get; set; }

        [Required(ErrorMessage = "Please enter a valid item id.")]
        [DataType(DataType.Text)]
        public string ItemId { get; set; } = "10048";

        public int LocationId { get; set; } = 1;

        [Required(ErrorMessage = "Please enter a whole number.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value between {1} and {2}.")]
        public int ItemQuantity { get; set; } = 25;
    }
}
