using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTC_Lego.Server.Migrations
{
    public partial class ChangeTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Locations_LocationId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Products_ProductId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Warehouses_WarehouseId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_Inventories_InventoryId",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetails_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Suppliers_SupplierId",
                table: "PurchaseOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetails_Inventories_InventoryId",
                table: "SaleOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetails_SaleOrders_SaleOrderId",
                table: "SaleOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrders_Customers_CustomerId",
                table: "SaleOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrders",
                table: "SaleOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrderDetails",
                table: "SaleOrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrderDetails",
                table: "PurchaseOrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Warehouses",
                newName: "Warehouse");

            migrationBuilder.RenameTable(
                name: "Suppliers",
                newName: "Supplier");

            migrationBuilder.RenameTable(
                name: "SaleOrders",
                newName: "SaleOrder");

            migrationBuilder.RenameTable(
                name: "SaleOrderDetails",
                newName: "SaleOrderDetail");

            migrationBuilder.RenameTable(
                name: "PurchaseOrders",
                newName: "PurchaseOrder");

            migrationBuilder.RenameTable(
                name: "PurchaseOrderDetails",
                newName: "PurchaseOrderDetail");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Product");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "Inventories",
                newName: "Inventory");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrders_CustomerId",
                table: "SaleOrder",
                newName: "IX_SaleOrder_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrderDetails_SaleOrderId",
                table: "SaleOrderDetail",
                newName: "IX_SaleOrderDetail_SaleOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrderDetails_InventoryId",
                table: "SaleOrderDetail",
                newName: "IX_SaleOrderDetail_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrder",
                newName: "IX_PurchaseOrder_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderDetails_PurchaseOrderId",
                table: "PurchaseOrderDetail",
                newName: "IX_PurchaseOrderDetail_PurchaseOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderDetails_InventoryId",
                table: "PurchaseOrderDetail",
                newName: "IX_PurchaseOrderDetail_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_WarehouseId",
                table: "Location",
                newName: "IX_Location_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_ProductId",
                table: "Inventory",
                newName: "IX_Inventory_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_LocationId",
                table: "Inventory",
                newName: "IX_Inventory_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse",
                column: "WarehouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrder",
                table: "SaleOrder",
                column: "SaleOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrderDetail",
                table: "SaleOrderDetail",
                column: "SaleOrderDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrder",
                table: "PurchaseOrder",
                column: "PurchaseOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrderDetail",
                table: "PurchaseOrderDetail",
                column: "PurchaseOrderDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Product",
                table: "Product",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                column: "InventoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Location_LocationId",
                table: "Inventory",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Product_ProductId",
                table: "Inventory",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Warehouse_WarehouseId",
                table: "Location",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Supplier_SupplierId",
                table: "PurchaseOrder",
                column: "SupplierId",
                principalTable: "Supplier",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetail_Inventory_InventoryId",
                table: "PurchaseOrderDetail",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetail_PurchaseOrder_PurchaseOrderId",
                table: "PurchaseOrderDetail",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrder",
                principalColumn: "PurchaseOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrder_Customer_CustomerId",
                table: "SaleOrder",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetail_Inventory_InventoryId",
                table: "SaleOrderDetail",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetail_SaleOrder_SaleOrderId",
                table: "SaleOrderDetail",
                column: "SaleOrderId",
                principalTable: "SaleOrder",
                principalColumn: "SaleOrderId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Location_LocationId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Product_ProductId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Location_Warehouse_WarehouseId",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Supplier_SupplierId",
                table: "PurchaseOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetail_Inventory_InventoryId",
                table: "PurchaseOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderDetail_PurchaseOrder_PurchaseOrderId",
                table: "PurchaseOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrder_Customer_CustomerId",
                table: "SaleOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetail_Inventory_InventoryId",
                table: "SaleOrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrderDetail_SaleOrder_SaleOrderId",
                table: "SaleOrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouse",
                table: "Warehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrderDetail",
                table: "SaleOrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrder",
                table: "SaleOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrderDetail",
                table: "PurchaseOrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseOrder",
                table: "PurchaseOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Product",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.RenameTable(
                name: "Warehouse",
                newName: "Warehouses");

            migrationBuilder.RenameTable(
                name: "Supplier",
                newName: "Suppliers");

            migrationBuilder.RenameTable(
                name: "SaleOrderDetail",
                newName: "SaleOrderDetails");

            migrationBuilder.RenameTable(
                name: "SaleOrder",
                newName: "SaleOrders");

            migrationBuilder.RenameTable(
                name: "PurchaseOrderDetail",
                newName: "PurchaseOrderDetails");

            migrationBuilder.RenameTable(
                name: "PurchaseOrder",
                newName: "PurchaseOrders");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "Inventory",
                newName: "Inventories");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrderDetail_SaleOrderId",
                table: "SaleOrderDetails",
                newName: "IX_SaleOrderDetails_SaleOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrderDetail_InventoryId",
                table: "SaleOrderDetails",
                newName: "IX_SaleOrderDetails_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrder_CustomerId",
                table: "SaleOrders",
                newName: "IX_SaleOrders_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderDetail_PurchaseOrderId",
                table: "PurchaseOrderDetails",
                newName: "IX_PurchaseOrderDetails_PurchaseOrderId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrderDetail_InventoryId",
                table: "PurchaseOrderDetails",
                newName: "IX_PurchaseOrderDetails_InventoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrder_SupplierId",
                table: "PurchaseOrders",
                newName: "IX_PurchaseOrders_SupplierId");

            migrationBuilder.RenameIndex(
                name: "IX_Location_WarehouseId",
                table: "Locations",
                newName: "IX_Locations_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventories",
                newName: "IX_Inventories_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_LocationId",
                table: "Inventories",
                newName: "IX_Inventories_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses",
                column: "WarehouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suppliers",
                table: "Suppliers",
                column: "SupplierId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrderDetails",
                table: "SaleOrderDetails",
                column: "SaleOrderDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrders",
                table: "SaleOrders",
                column: "SaleOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrderDetails",
                table: "PurchaseOrderDetails",
                column: "PurchaseOrderDetailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseOrders",
                table: "PurchaseOrders",
                column: "PurchaseOrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "InventoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Locations_LocationId",
                table: "Inventories",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Products_ProductId",
                table: "Inventories",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Warehouses_WarehouseId",
                table: "Locations",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "WarehouseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_Inventories_InventoryId",
                table: "PurchaseOrderDetails",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderDetails_PurchaseOrders_PurchaseOrderId",
                table: "PurchaseOrderDetails",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "PurchaseOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Suppliers_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "SupplierId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetails_Inventories_InventoryId",
                table: "SaleOrderDetails",
                column: "InventoryId",
                principalTable: "Inventories",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrderDetails_SaleOrders_SaleOrderId",
                table: "SaleOrderDetails",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "SaleOrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrders_Customers_CustomerId",
                table: "SaleOrders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
