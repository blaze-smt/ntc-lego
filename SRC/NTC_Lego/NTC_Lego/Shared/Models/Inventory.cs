namespace NTC_Lego.Shared
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int InventoryQuantity { get; set; }
        public decimal InventoryPrice{ get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
        public string ItemId { get; set; }
        public Item Item { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
