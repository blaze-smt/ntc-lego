using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NTC_Lego.Server.Migrations
{
    public partial class orderstatus1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "SaleOrder");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "PurchaseOrder");

            migrationBuilder.RenameColumn(
                name: "ShippingStatus",
                table: "SaleOrder",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "ShippingStatus",
                table: "PurchaseOrder",
                newName: "OrderStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "SaleOrder",
                newName: "ShippingStatus");

            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                table: "PurchaseOrder",
                newName: "ShippingStatus");

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "SaleOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaymentStatus",
                table: "PurchaseOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
