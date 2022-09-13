using Microsoft.EntityFrameworkCore;
using NTC_Lego.Shared;

namespace NTC_Lego.Server
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) 
            : base(options)
        { }
        
        // Create tables
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specify table seed data below
            // Be mindful of foreign key contraints
            // (e.g. you will need to populate Warehouses before Locations)
            /*
            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { WarehouseId = 1, WarehouseName = "Lego Supply" },
                new Warehouse { WarehouseId = 2, WarehouseName = "Brick Depot" }
            );
            */
            // When you are done adding the data, run another migration (e.g. dotnet ef migrations add SeedData).

            //Product Data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    ProductName = "Starwar Men",
                    ProductDescription = "A male starwar lego",
                    ProductColor = "Black",
                    ProductWeight = 0.5m,
                    ProductPrice = 4.99m
                },

                new Product
                {
                    ProductId = 2,
                    ProductName = "Red Block 6",
                    ProductDescription = "A red block with 6 dot",
                    ProductColor = "Black",
                    ProductWeight = 0.3m,
                    ProductPrice = 1.99m
                }
                );

            //Warehouse starting Data
            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { WarehouseId = 1, WarehouseName = "Wausau Storage"},
                new Warehouse { WarehouseId = 2, WarehouseName = "Merrill Storage"}
                );

            //Location starting Data
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, BinName = "1A", WarehouseId = 1},
                new Location { LocationId = 2, BinName = "2B", WarehouseId = 1},
                new Location { LocationId = 3, BinName = "1AD", WarehouseId = 2}
                );

            //Inventories Starting Data
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { InventoryId = 1, InventoryQuantity = 100, ProductId = 1, LocationId =1},
                new Inventory { InventoryId = 2, InventoryQuantity = 89, ProductId = 2, LocationId = 2},
                new Inventory { InventoryId = 3, InventoryQuantity = 99, ProductId = 1, LocationId = 3}
                );

            //Customer Starting Data
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, CustomerName = "Zelda"},
                new Customer { CustomerId = 2, CustomerName = "Jackie Jason"}
                );

            //SaleOrders Starting Data
            modelBuilder.Entity<SaleOrder>().HasData(
                new SaleOrder { SaleOrderId = 1, SaleOrderDate = new DateTime(2022, 9, 12, 8, 0, 0), SaleOrderTotalPrice = 4.99m, CustomerId = 1}
                );

            //SaleOrderDetail Starting Data
            modelBuilder.Entity<SaleOrderDetail>().HasData(
                new SaleOrderDetail { SaleOrderDetailId = 1, SaleOrderDetailQuantity = 10, SaleOrderSubTotalPrice = 49.90m , SaleOrderId = 1, InventoryId = 1}
                );

            //Suppliers Starting Data
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier { SupplierId = 1, SupplierName = "Super Toy"}
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
