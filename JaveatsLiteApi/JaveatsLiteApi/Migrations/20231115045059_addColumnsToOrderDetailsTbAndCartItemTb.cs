using Microsoft.EntityFrameworkCore.Migrations;

namespace JaveatsLiteApi.Migrations
{
    public partial class addColumnsToOrderDetailsTbAndCartItemTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ShoppingCartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "ShoppingCartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<decimal>(
                name: "OrderTotal",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "OrderTotal",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
