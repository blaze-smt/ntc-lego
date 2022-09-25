namespace NTC_Lego.Shared
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public ICollection<Location> Locations { get; set; }
    }
}
