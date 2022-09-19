using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTC_Lego.Server.Migrations
{
    public partial class changeCustomerToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrder_Customer_CustomerId",
                table: "SaleOrder");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "SaleOrder",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrder_CustomerId",
                table: "SaleOrder",
                newName: "IX_SaleOrder_UserId");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "SupplierId",
                keyValue: 1,
                column: "SupplierName",
                value: "Super Toy Inc.");

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "UserId", "UserName" },
                values: new object[,]
                {
                    { 1, "ZeldaFan2022" },
                    { 2, "JackieJason" }
                });

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "WarehouseId",
                keyValue: 2,
                column: "WarehouseName",
                value: "Merrill Supply");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrder_User_UserId",
                table: "SaleOrder",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrder_User_UserId",
                table: "SaleOrder");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SaleOrder",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleOrder_UserId",
                table: "SaleOrder",
                newName: "IX_SaleOrder_CustomerId");

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "CustomerId", "CustomerName" },
                values: new object[,]
                {
                    { 1, "Zelda" },
                    { 2, "Jackie Jason" }
                });

            migrationBuilder.UpdateData(
                table: "Supplier",
                keyColumn: "SupplierId",
                keyValue: 1,
                column: "SupplierName",
                value: "Super Toy");

            migrationBuilder.UpdateData(
                table: "Warehouse",
                keyColumn: "WarehouseId",
                keyValue: 2,
                column: "WarehouseName",
                value: "Merrill Storage");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrder_Customer_CustomerId",
                table: "SaleOrder",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
