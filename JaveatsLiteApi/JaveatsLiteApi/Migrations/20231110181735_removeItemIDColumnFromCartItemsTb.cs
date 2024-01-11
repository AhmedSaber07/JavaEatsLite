using Microsoft.EntityFrameworkCore.Migrations;

namespace JaveatsLiteApi.Migrations
{
    public partial class removeItemIDColumnFromCartItemsTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Items_ItemID",
                table: "ShoppingCartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "ShoppingCartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Items_ItemID",
                table: "ShoppingCartItems",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Items_ItemID",
                table: "ShoppingCartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Items_ItemID",
                table: "ShoppingCartItems",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
