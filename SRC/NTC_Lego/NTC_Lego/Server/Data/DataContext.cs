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

        // Create tables from models
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

            // Define the foreign key relationships below
            modelBuilder.Entity<Item>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Items);

            modelBuilder.Entity<Item>()
                .HasOne(x => x.ItemType)
                .WithMany(x => x.Items);

            modelBuilder.Entity<PurchaseOrder>()
                .HasOne(x => x.Supplier)
                .WithMany(x => x.PurchaseOrders);

            modelBuilder.Entity<PurchaseOrderDetail>()
                .HasOne(x => x.PurchaseOrder)
                .WithMany(x => x.PurchaseOrderDetails);

            modelBuilder.Entity<PurchaseOrderDetail>()
                .HasOne(x => x.Inventory)
                .WithMany(x => x.PurchaseOrderDetails);

            modelBuilder.Entity<SaleOrder>()
                .HasOne(x => x.User)
                .WithMany(x => x.SaleOrders);

            modelBuilder.Entity<SaleOrderDetail>()
                .HasOne(x => x.SaleOrder)
                .WithMany(x => x.SaleOrderDetails);

            modelBuilder.Entity<SaleOrderDetail>()
                .HasOne(x => x.Inventory)
                .WithMany(x => x.SaleOrderDetails);

            modelBuilder.Entity<Inventory>()
                .HasOne(x => x.Location)
                .WithMany(x => x.Inventories);

            modelBuilder.Entity<Location>()
                .HasOne(x => x.Warehouse)
                .WithMany(x => x.Locations);

            // Specify table seed data below
            /*
            modelBuilder.Entity<Warehouse>().HasData(
                new Warehouse { WarehouseId = 1, WarehouseName = "Wausau Storage"},
                new Warehouse { WarehouseId = 2, WarehouseName = "Merrill Supply"}
                );
            */
        }
    }
}
