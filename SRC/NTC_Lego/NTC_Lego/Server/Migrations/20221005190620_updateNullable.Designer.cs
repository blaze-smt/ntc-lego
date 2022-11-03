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
    [Migration("20221005190620_updateNullable")]
    partial class updateNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NTC_Lego.Shared.Inventory", b =>
                {
                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryId"), 1L, 1);

                    b.Property<int>("InventoryQuantity")
                        .HasColumnType("int");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("InventoryId");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProductId");

                    b.ToTable("Inventory");

                    b.HasData(
                        new
                        {
                            InventoryId = 1,
                            InventoryQuantity = 100,
                            LocationId = 1,
                            ProductId = 1
                        },
                        new
                        {
                            InventoryId = 2,
                            InventoryQuantity = 89,
                            LocationId = 2,
                            ProductId = 2
                        },
                        new
                        {
                            InventoryId = 3,
                            InventoryQuantity = 99,
                            LocationId = 3,
                            ProductId = 1
                        });
                });

            modelBuilder.Entity("NTC_Lego.Shared.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationId"), 1L, 1);

                    b.Property<string>("BinName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("LocationId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Location");

                    b.HasData(
                        new
                        {
                            LocationId = 1,
                            BinName = "1A",
                            WarehouseId = 1
                        },
                        new
                        {
                            LocationId = 2,
                            BinName = "2B",
                            WarehouseId = 1
                        },
                        new
                        {
                            LocationId = 3,
                            BinName = "1AD",
                            WarehouseId = 2
                        });
                });

            modelBuilder.Entity("NTC_Lego.Shared.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<string>("ProductColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ProductWeight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            ProductColor = "Black",
                            ProductDescription = "A male starwar lego",
                            ProductName = "Starwar Men",
                            ProductPrice = 4.99m,
                            ProductWeight = 0.5m
                        },
                        new
                        {
                            ProductId = 2,
                            ProductColor = "Black",
                            ProductDescription = "A red block with 6 dot",
                            ProductName = "Red Block 6",
                            ProductPrice = 1.99m,
                            ProductWeight = 0.3m
                        });
                });

            modelBuilder.Entity("NTC_Lego.Shared.PurchaseOrder", b =>
                {
                    b.Property<int>("PurchaseOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseOrderId"), 1L, 1);

                    b.Property<DateTime>("PurchaseOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PurchaseOrderTotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("PurchaseOrderId");

                    b.HasIndex("SupplierId");

                    b.ToTable("PurchaseOrder");

                    b.HasData(
                        new
                        {
                            PurchaseOrderId = 1,
                            PurchaseOrderDate = new DateTime(2022, 9, 11, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            PurchaseOrderTotalPrice = 1.99m,
                            SupplierId = 1
                        });
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

                    b.Property<decimal>("PurchaseOrderSubTotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PurchaseOrderDetailId");

                    b.HasIndex("InventoryId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("PurchaseOrderDetail");

                    b.HasData(
                        new
                        {
                            PurchaseOrderDetailId = 1,
                            InventoryId = 1,
                            PurchaseOrderDetailQuantity = 100,
                            PurchaseOrderId = 1,
                            PurchaseOrderSubTotalPrice = 199m
                        });
                });

            modelBuilder.Entity("NTC_Lego.Shared.SaleOrder", b =>
                {
                    b.Property<int>("SaleOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleOrderId"), 1L, 1);

                    b.Property<DateTime>("SaleOrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SaleOrderTotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SaleOrderId");

                    b.HasIndex("UserId");

                    b.ToTable("SaleOrder");

                    b.HasData(
                        new
                        {
                            SaleOrderId = 1,
                            SaleOrderDate = new DateTime(2022, 9, 12, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            SaleOrderTotalPrice = 4.99m,
                            UserId = 1
                        });
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

                    b.Property<decimal>("SaleOrderSubTotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SaleOrderDetailId");

                    b.HasIndex("InventoryId");

                    b.HasIndex("SaleOrderId");

                    b.ToTable("SaleOrderDetail");

                    b.HasData(
                        new
                        {
                            SaleOrderDetailId = 1,
                            InventoryId = 1,
                            SaleOrderDetailQuantity = 10,
                            SaleOrderId = 1,
                            SaleOrderSubTotalPrice = 49.90m
                        });
                });

            modelBuilder.Entity("NTC_Lego.Shared.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierId"), 1L, 1);

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierId");

                    b.ToTable("Supplier");

                    b.HasData(
                        new
                        {
                            SupplierId = 1,
                            SupplierName = "Super Toy Inc."
                        });
                });

            modelBuilder.Entity("NTC_Lego.Shared.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            UserName = "ZeldaFan2022"
                        },
                        new
                        {
                            UserId = 2,
                            UserName = "JackieJason"
                        });
                });

            modelBuilder.Entity("NTC_Lego.Shared.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseId"), 1L, 1);

                    b.Property<string>("WarehouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouse");

                    b.HasData(
                        new
                        {
                            WarehouseId = 1,
                            WarehouseName = "Wausau Storage"
                        },
                        new
                        {
                            WarehouseId = 2,
                            WarehouseName = "Merrill Supply"
                        });
                });

            modelBuilder.Entity("NTC_Lego.Shared.Inventory", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NTC_Lego.Shared.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("NTC_Lego.Shared.Location", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("NTC_Lego.Shared.PurchaseOrder", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("NTC_Lego.Shared.PurchaseOrderDetail", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NTC_Lego.Shared.PurchaseOrder", "PurchaseOrder")
                        .WithMany()
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("PurchaseOrder");
                });

            modelBuilder.Entity("NTC_Lego.Shared.SaleOrder", b =>
                {
                    b.HasOne("NTC_Lego.Shared.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NTC_Lego.Shared.SaleOrderDetail", b =>
                {
                    b.HasOne("NTC_Lego.Shared.Inventory", "Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NTC_Lego.Shared.SaleOrder", "SaleOrder")
                        .WithMany()
                        .HasForeignKey("SaleOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("SaleOrder");
                });
#pragma warning restore 612, 618
        }
    }
}
