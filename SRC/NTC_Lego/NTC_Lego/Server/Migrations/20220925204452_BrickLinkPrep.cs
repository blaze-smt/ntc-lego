using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTC_Lego.Server.Migrations
{
    public partial class BrickLinkPrep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Product_ProductId",
                table: "Inventory");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Inventory",
                newName: "ColorId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory",
                newName: "IX_Inventory_ColorId");

            migrationBuilder.AddColumn<decimal>(
                name: "InventoryPrice",
                table: "Inventory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "Inventory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    ColorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "ItemType",
                columns: table => new
                {
                    ItemTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemType", x => x.ItemTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_ItemType_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "ItemType",
                        principalColumn: "ItemTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 39, "Tile, Decorated" },
                    { 85, "Homemaker" }
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "ColorId", "ColorName", "ColorType", "ColorValue" },
                values: new object[,]
                {
                    { 11, "Black", "Solid", "212121" },
                    { 41, "Aqua", "Solid", "BCE5DC" }
                });

            migrationBuilder.InsertData(
                table: "ItemType",
                columns: new[] { "ItemTypeId", "ItemTypeName" },
                values: new object[,]
                {
                    { "P", "Part" },
                    { "S", "Set" }
                });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "CategoryId", "ItemName", "ItemTypeId", "ItemWeight" },
                values: new object[] { "1", 85, "Homemaker Bookcase 2 x 4 x 4", "P", 5.8m });

            migrationBuilder.InsertData(
                table: "Item",
                columns: new[] { "ItemId", "CategoryId", "ItemName", "ItemTypeId", "ItemWeight" },
                values: new object[] { "3068bpb0058", 39, "Tile 2 x 2 with Groove with Computer Monitor with White Circle and Line Power Switch Pattern", "P", 0.45m });

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "InventoryId",
                keyValue: 1,
                columns: new[] { "ColorId", "InventoryPrice", "ItemId" },
                values: new object[] { 41, 4.25m, "1" });

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "InventoryId",
                keyValue: 2,
                columns: new[] { "ColorId", "InventoryPrice", "ItemId" },
                values: new object[] { 11, 1.15m, "3068bpb0058" });

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "InventoryId",
                keyValue: 3,
                columns: new[] { "ColorId", "InventoryPrice", "ItemId" },
                values: new object[] { 11, 4.00m, "1" });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ItemId",
                table: "Inventory",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_CategoryId",
                table: "Item",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemTypeId",
                table: "Item",
                column: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Color_ColorId",
                table: "Inventory",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Item_ItemId",
                table: "Inventory",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Color_ColorId",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Item_ItemId",
                table: "Inventory");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "ItemType");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_ItemId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "InventoryPrice",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Inventory");

            migrationBuilder.RenameColumn(
                name: "ColorId",
                table: "Inventory",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_ColorId",
                table: "Inventory",
                newName: "IX_Inventory_ProductId");

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "ProductColor", "ProductDescription", "ProductName", "ProductPrice", "ProductWeight" },
                values: new object[] { 1, "Black", "A male starwar lego", "Starwar Men", 4.99m, 0.5m });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "ProductColor", "ProductDescription", "ProductName", "ProductPrice", "ProductWeight" },
                values: new object[] { 2, "Black", "A red block with 6 dot", "Red Block 6", 1.99m, 0.3m });

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "InventoryId",
                keyValue: 1,
                column: "ProductId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "InventoryId",
                keyValue: 2,
                column: "ProductId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "InventoryId",
                keyValue: 3,
                column: "ProductId",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Product_ProductId",
                table: "Inventory",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
