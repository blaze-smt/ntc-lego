namespace NTC_Lego.Shared
{
    public class Location
    {
        public int LocationId { get; set; }
        public string BinName { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
    }
}
