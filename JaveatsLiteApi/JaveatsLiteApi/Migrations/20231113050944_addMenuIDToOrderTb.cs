using Microsoft.EntityFrameworkCore.Migrations;

namespace JaveatsLiteApi.Migrations
{
    public partial class addMenuIDToOrderTb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuID",
                table: "Orders");
        }
    }
}
