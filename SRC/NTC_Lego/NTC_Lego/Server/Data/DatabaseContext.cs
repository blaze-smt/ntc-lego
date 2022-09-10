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

        }
    }
}
