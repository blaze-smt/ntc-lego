using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTC_Lego.Server.Migrations
{
    public partial class RemoveSomeSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "ColorId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Color",
                keyColumn: "ColorId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "ItemId",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Item",
                keyColumn: "ItemId",
                keyValue: "3068bpb0058");

            migrationBuilder.DeleteData(
                table: "ItemType",
                keyColumn: "ItemTypeId",
                keyValue: "S");

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "ItemType",
                keyColumn: "ItemTypeId",
                keyValue: "P");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
