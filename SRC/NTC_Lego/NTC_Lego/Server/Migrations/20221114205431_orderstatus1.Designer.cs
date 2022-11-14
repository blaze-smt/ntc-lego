﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NTC_Lego.Server;

#nullable disable

namespace NTC_Lego.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221114205431_orderstatus1")]
    partial class orderstatus1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NTC_Lego.Shared.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Color", b =>
                {
                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ColorType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ColorValue")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ColorId");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Inventory", b =>
                {
                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryId"), 1L, 1);

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<decimal>("InventoryItemPrice")
                        .HasColumnType("decimal(10,4)");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("InventoryId");

                    b.HasIndex("ColorId");

                    b.HasIndex("ItemId", "ColorId")
                        .IsUnique();

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("NTC_Lego.Shared.InventoryLocation", b =>
                {
                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("ItemQuantity")
                        .HasColumnType("int");

                    b.HasKey("InventoryId", "LocationId");

                    b.HasIndex("LocationId");

                    b.ToTable("InventoryLocation");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Item", b =>
                {
                    b.Property<string>("ItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ItemTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<double?>("ItemWeight")
                        .HasColumnType("float");

                    b.HasKey("ItemId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ItemTypeId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("NTC_Lego.Shared.ItemType", b =>
                {
                    b.Property<string>("ItemTypeId")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("ItemTypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ItemTypeId");

                    b.ToTable("ItemType");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationId"), 1L, 1);

                    b.Property<string>("BinName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("LocationId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("NTC_Lego.Shared.PurchaseOrder", b =>
                {
                    b.Property<int>("PurchaseOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseOrderId"), 1L, 1);

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PurchaseOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("PurchaseOrderId");

                    b.HasIndex("SupplierId");

                    b.ToTable("PurchaseOrder");
                });

            modelBuilder.Entity("NTC_Lego.Shared.PurchaseOrderDetail", b =>
                {
                    b.Property<int>("PurchaseOrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseOrderDetailId"), 1L, 1);

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<int>("PurchaseOrderDetailQuantity")
                        .HasColumnType("int");

                    b.Property<int>("PurchaseOrderId")
                        .HasColumnType("int");

                    b.HasKey("PurchaseOrderDetailId");

                    b.HasIndex("InventoryId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("PurchaseOrderDetail");
                });

            modelBuilder.Entity("NTC_Lego.Shared.SaleOrder", b =>
                {
                    b.Property<int>("SaleOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleOrderId"), 1L, 1);

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SaleOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SaleOrderId");

                    b.HasIndex("UserId");

                    b.ToTable("SaleOrder");
                });

            modelBuilder.Entity("NTC_Lego.Shared.SaleOrderDetail", b =>
                {
                    b.Property<int>("SaleOrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleOrderDetailId"), 1L, 1);

                    b.Property<int>("InventoryId")
                        .HasColumnType("int");

                    b.Property<int>("SaleOrderDetailQuantity")
                        .HasColumnType("int");

                    b.Property<int>("SaleOrderId")
                        .HasColumnType("int");

                    b.HasKey("SaleOrderDetailId");

                    b.HasIndex("InventoryId");

                    b.HasIndex("SaleOrderId");

                    b.ToTable("SaleOrderDetail");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierId"), 1L, 1);

                    b.Property<string>("SupplierEmail")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SupplierId");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("NTC_Lego.Shared.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Address2")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("State")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Zip")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseId"), 1L, 1);

                    b.Property<string>("WarehouseName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Inventory", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NTC_Lego.Shared.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("NTC_Lego.Shared.InventoryLocation", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Inventory", "Inventory")
                        .WithMany("InventoryLocations")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NTC_Lego.Shared.Location", "Location")
                        .WithMany("InventoryLocations")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Item", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NTC_Lego.Shared.ItemType", "ItemType")
                        .WithMany("Items")
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("ItemType");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Location", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Warehouse", "Warehouse")
                        .WithMany("Locations")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("NTC_Lego.Shared.PurchaseOrder", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Supplier", "Supplier")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("NTC_Lego.Shared.PurchaseOrderDetail", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Inventory", "Inventory")
                        .WithMany("PurchaseOrderDetails")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NTC_Lego.Shared.PurchaseOrder", "PurchaseOrder")
                        .WithMany("PurchaseOrderDetails")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("PurchaseOrder");
                });

            modelBuilder.Entity("NTC_Lego.Shared.SaleOrder", b =>
                {
                    b.HasOne("NTC_Lego.Shared.User", "User")
                        .WithMany("SaleOrders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NTC_Lego.Shared.SaleOrderDetail", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Inventory", "Inventory")
                        .WithMany("SaleOrderDetails")
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NTC_Lego.Shared.SaleOrder", "SaleOrder")
                        .WithMany("SaleOrderDetails")
                        .HasForeignKey("SaleOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("SaleOrder");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Category", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Inventory", b =>
                {
                    b.Navigation("InventoryLocations");

                    b.Navigation("PurchaseOrderDetails");

                    b.Navigation("SaleOrderDetails");
                });

            modelBuilder.Entity("NTC_Lego.Shared.ItemType", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Location", b =>
                {
                    b.Navigation("InventoryLocations");
                });

            modelBuilder.Entity("NTC_Lego.Shared.PurchaseOrder", b =>
                {
                    b.Navigation("PurchaseOrderDetails");
                });

            modelBuilder.Entity("NTC_Lego.Shared.SaleOrder", b =>
                {
                    b.Navigation("SaleOrderDetails");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Supplier", b =>
                {
                    b.Navigation("PurchaseOrders");
                });

            modelBuilder.Entity("NTC_Lego.Shared.User", b =>
                {
                    b.Navigation("SaleOrders");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Warehouse", b =>
                {
                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
