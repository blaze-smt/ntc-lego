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

            // Specify table seed data below
            // Be mindful of foreign key contraints
            // (e.g. you will need to populate Warehouses before Locations)
            // When you are done adding the data, run another migration from a terminal in the server project (e.g. dotnet ef migrations add SeedData).

            //Category Starting Data
            modelBuilder.Entity<Category>();

            //ItemType Starting Data
            modelBuilder.Entity<ItemType>();

            //Item Starting Data
            modelBuilder.Entity<Item>();

            //Color Starting Data
            modelBuilder.Entity<Color>();

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

            //Inventories Starting Data
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { InventoryId = 1, InventoryQuantity = 100, InventoryPrice = 4.25m, ColorId = 41, ItemId = "1", LocationId =1},
                new Inventory { InventoryId = 2, InventoryQuantity = 89, InventoryPrice = 1.15m, ColorId = 11, ItemId = "3068bpb0058", LocationId = 2},
                new Inventory { InventoryId = 3, InventoryQuantity = 99, InventoryPrice = 4.00m, ColorId = 11, ItemId = "1", LocationId = 3}
                );

            //Customer Starting Data
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, UserName = "ZeldaFan2022"},
                new User { UserId = 2, UserName = "JackieJason"}
                );

            //SaleOrders Starting Data
            modelBuilder.Entity<SaleOrder>().HasData(
                new SaleOrder { SaleOrderId = 1, SaleOrderDate = new DateTime(2022, 9, 12, 8, 0, 0), SaleOrderTotalPrice = 4.99m, UserId = 1}
                );

            //SaleOrderDetail Starting Data
            modelBuilder.Entity<SaleOrderDetail>().HasData(
                new SaleOrderDetail { SaleOrderDetailId = 1, SaleOrderDetailQuantity = 10, SaleOrderSubTotalPrice = 49.90m , SaleOrderId = 1, InventoryId = 1}
                );

            //Suppliers Starting Data
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = 1, SupplierName = "Super Toy Inc."}
                );

            //PuchaseOrders Starting Data
            modelBuilder.Entity<PurchaseOrder>().HasData(
                new PurchaseOrder { PurchaseOrderId = 1, PurchaseOrderDate = new DateTime(2022, 9, 11, 8, 0, 0), PurchaseOrderTotalPrice = 1.99m, SupplierId = 1}
                );

            //PurchaseOrderDeatails Starting Data
            modelBuilder.Entity<PurchaseOrderDetail>().HasData(
                new PurchaseOrderDetail { PurchaseOrderDetailId = 1, PurchaseOrderDetailQuantity = 100, PurchaseOrderSubTotalPrice = 199m, PurchaseOrderId = 1, InventoryId = 1}
                );
        }
    }
}
