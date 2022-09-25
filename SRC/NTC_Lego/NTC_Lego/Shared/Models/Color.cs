namespace NTC_Lego.Shared
{
    public class Color
    {
        public int ColorId { get; set; }
        public string? ColorName { get; set; }
        public string? ColorValue { get; set; }
        public string? ColorType { get; set; }
        public ICollection<Inventory> Inventories { get; set; }
    }
}
