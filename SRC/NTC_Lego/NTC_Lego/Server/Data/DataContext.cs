using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

using Microsoft.EntityFrameworkCore;
using NTC_Lego.Shared;

namespace NTC_Lego.Server
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        // Create tables
        public DbSet<Category> Category { get; set; }
        public DbSet<ItemType> ItemType { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<SaleOrder> SaleOrder { get; set; }
        public DbSet<SaleOrderDetail> SaleOrderDetail { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // // Specify table seed data below
            
            //ItemType Starting Data (BrickLink XML)
            var itemTypes = this.getItemTypeXML();
            modelBuilder.Entity<ItemType>().HasData(itemTypes);

            //Color Starting Data (BrickLink XML)
            var colors = this.getColorXML();
            modelBuilder.Entity<Color>().HasData(colors);

            //Category Starting Data (BrickLink XML)
            var categories = this.getCategoryXML();
            modelBuilder.Entity<Category>().HasData(categories);

            //Item Starting Data (BrickLink XML)
            var sets = this.getSetsXML();
            modelBuilder.Entity<Item>().HasData(sets);
            //var parts = this.getPartsXML();
            //modelBuilder.Entity<Item>().HasData(parts);

            //Warehouse starting Data
            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { WarehouseId = 1, WarehouseName = "Wausau Storage"},
                new Warehouse { WarehouseId = 2, WarehouseName = "Merrill Supply"}
                );

            //Location starting Data
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, BinName = "1A", WarehouseId = 1},
                new Location { LocationId = 2, BinName = "2B", WarehouseId = 1},
                new Location { LocationId = 3, BinName = "1AD", WarehouseId = 2}
                );

            //3068bpb0058
            //Inventories Starting Data
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { InventoryId = 1, InventoryQuantity = 20, InventoryItemPrice = 155.95m, ColorId = 0, ItemId = "10297-1", LocationId = 1},
                new Inventory { InventoryId = 2, InventoryQuantity = 89, InventoryItemPrice = 1.20m, ColorId = 11, ItemId = "60352-6", LocationId = 2},
                new Inventory { InventoryId = 3, InventoryQuantity = 99, InventoryItemPrice = 155.95m, ColorId = 0, ItemId = "10297-1", LocationId = 3}
                );

            //Customer Starting Data
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "ZeldaFan2022"},
                new User { UserId = 2, UserName = "JackieJason"}
                );

            //SaleOrders Starting Data
            modelBuilder.Entity<SaleOrder>().HasData(
                new SaleOrder { SaleOrderId = 1, SaleOrderDate = new DateTime(2022, 9, 12, 8, 0, 0), UserId = 1}
                );

            //SaleOrderDetail Starting Data
            modelBuilder.Entity<SaleOrderDetail>().HasData(
                new SaleOrderDetail { SaleOrderDetailId = 1, SaleOrderDetailQuantity = 10, SaleOrderId = 1, InventoryId = 1}
                );

            //Suppliers Starting Data
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = 1, SupplierName = "Super Toy Inc."}
                );

            //PuchaseOrders Starting Data
            modelBuilder.Entity<PurchaseOrder>().HasData(
                new PurchaseOrder { PurchaseOrderId = 1, PurchaseOrderDate = new DateTime(2022, 9, 11, 8, 0, 0), SupplierId = 1}
                );

            //PurchaseOrderDeatails Starting Data
            modelBuilder.Entity<PurchaseOrderDetail>().HasData(
                new PurchaseOrderDetail { PurchaseOrderDetailId = 1, PurchaseOrderDetailQuantity = 100, PurchaseOrderId = 1, InventoryId = 1}
                ); 
        }

        private List<ItemType> getItemTypeXML()
        {
            // Get path to XML file 
            string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, @"BrickLinkXML\itemtypes.xml");

            // Read XML file
            XDocument xdoc = XDocument.Load(path);

            // Map XML file to list
            List<ItemType> itemTypes = xdoc.Descendants("ITEM").Select(x => new ItemType()
            {
                ItemTypeId = x.Element("ITEMTYPE").Value,
                ItemTypeName = x.Element("ITEMTYPENAME").Value,
            }).ToList();

            return itemTypes;
        }

        private List<Color> getColorXML()
        {
            // Get path to XML file 
            string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, @"BrickLinkXML\colors.xml");

            // Read XML file
            XDocument xdoc = XDocument.Load(path);

            // Map XML file to list
            List<Color> colors = xdoc.Descendants("ITEM").Select(x => new Color()
            {
                ColorId = Convert.ToInt32(x.Element("COLOR").Value),
                ColorName = x.Element("COLORNAME").Value,
                ColorValue = x.Element("COLORRGB").Value,
                ColorType = x.Element("COLORTYPE").Value,
            }).ToList();

            return colors;
        }

        private List<Category> getCategoryXML()
        {
            // Get path to XML file 
            string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, @"BrickLinkXML\categories.xml");

            // Read XML file
            XDocument xdoc = XDocument.Load(path);

            // Map XML file to list
            List<Category> categories = xdoc.Descendants("ITEM").Select(x => new Category()
            {
                CategoryId = Convert.ToInt32(x.Element("CATEGORY").Value),
                CategoryName = x.Element("CATEGORYNAME").Value,
            }).ToList();

            return categories;
        }

        private List<Item> getSetsXML()
        {
            // Get path to XML file 
            string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, @"BrickLinkXML\Sets.xml");

            // Read XML file
            XDocument xdoc = XDocument.Load(path);

            // Map XML file to list
            List<Item> sets = xdoc.Descendants("ITEM").Select(x => new Item()
            {
                ItemId = x.Element("ITEMID").Value,
                ItemName = x.Element("ITEMNAME").Value,
                ItemWeight = x.Element("ITEMWEIGHT").Value == String.Empty ? 0m : Convert.ToDecimal(x.Element("ITEMWEIGHT").Value),
                ItemTypeId = x.Element("ITEMTYPE").Value,
                CategoryId = Convert.ToInt32(x.Element("CATEGORY").Value),
            }).ToList();

            return sets;
        }

        private List<Item> getPartsXML()
        {
            // Get path to XML file 
            string path = System.IO.Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName, @"BrickLinkXML\Parts.xml");

            // Read XML file
            XDocument xdoc = XDocument.Load(path);

            // Map XML file to list
            List<Item> parts = xdoc.Descendants("ITEM").Select(x => new Item()
            {
                ItemId = x.Element("ITEMID").Value,
                ItemName = x.Element("ITEMNAME").Value,
                ItemWeight = x.Element("ITEMWEIGHT").Value == String.Empty ? 0m : Convert.ToDecimal(x.Element("ITEMWEIGHT").Value),
                ItemTypeId = x.Element("ITEMTYPE").Value,
                CategoryId = Convert.ToInt32(x.Element("CATEGORY").Value),
            }).ToList();

            return parts;
        }
    }
}
