using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTC_Lego.Server.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "CustomerName" },
                values: new object[,]
                {
                    { 1, "Zelda" },
                    { 2, "Jackie Jason" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ProductColor", "ProductDescription", "ProductName", "ProductPrice", "ProductWeight" },
                values: new object[,]
                {
                    { 1, "Black", "A male starwar lego", "Starwar Men", 4.99m, 0.5m },
                    { 2, "Black", "A red block with 6 dot", "Red Block 6", 1.99m, 0.3m }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "SupplierName" },
                values: new object[] { 1, "Super Toy" });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "WarehouseId", "WarehouseName" },
                values: new object[,]
                {
                    { 1, "Wausau Storage" },
                    { 2, "Merrill Storage" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "BinName", "WarehouseId" },
                values: new object[,]
                {
                    { 1, "1A", 1 },
                    { 2, "2B", 1 },
                    { 3, "1AD", 2 }
                });

            migrationBuilder.InsertData(
                table: "PurchaseOrders",
                columns: new[] { "PurchaseOrderId", "PurchaseOrderDate", "PurchaseOrderTotalPrice", "SupplierId" },
                values: new object[] { 1, new DateTime(2022, 9, 11, 8, 0, 0, 0, DateTimeKind.Unspecified), 1.99m, 1 });

            migrationBuilder.InsertData(
                table: "SaleOrders",
                columns: new[] { "SaleOrderId", "CustomerId", "SaleOrderDate", "SaleOrderTotalPrice" },
                values: new object[] { 1, 1, new DateTime(2022, 9, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), 4.99m });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "InventoryId", "InventoryQuantity", "LocationId", "ProductId" },
                values: new object[] { 1, 100, 1, 1 });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "InventoryId", "InventoryQuantity", "LocationId", "ProductId" },
                values: new object[] { 2, 89, 2, 2 });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "InventoryId", "InventoryQuantity", "LocationId", "ProductId" },
                values: new object[] { 3, 99, 3, 1 });

            migrationBuilder.InsertData(
                table: "PurchaseOrderDetails",
                columns: new[] { "PurchaseOrderDetailId", "InventoryId", "PurchaseOrderDetailQuantity", "PurchaseOrderId", "PurchaseOrderSubTotalPrice" },
                values: new object[] { 1, 1, 100, 1, 199m });

            migrationBuilder.InsertData(
                table: "SaleOrderDetails",
                columns: new[] { "SaleOrderDetailId", "InventoryId", "SaleOrderDetailQuantity", "SaleOrderId", "SaleOrderSubTotalPrice" },
                values: new object[] { 1, 1, 10, 1, 49.90m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PurchaseOrderDetails",
                keyColumn: "PurchaseOrderDetailId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SaleOrderDetails",
                keyColumn: "SaleOrderDetailId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "InventoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PurchaseOrders",
                keyColumn: "PurchaseOrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SaleOrders",
                keyColumn: "SaleOrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Suppliers",
                keyColumn: "SupplierId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "WarehouseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "WarehouseId",
                keyValue: 1);
        }
    }
}
