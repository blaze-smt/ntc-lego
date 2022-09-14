namespace NTC_Lego.Shared
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int InventoryQuantity { get; set; }
        // FK Product
        public int ProductId { get; set; }
        public Product Product { get; set; }
        // FK Location
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
