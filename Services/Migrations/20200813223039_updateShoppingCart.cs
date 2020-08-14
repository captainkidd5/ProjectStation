using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    public partial class updateShoppingCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CheckedOut",
                table: "ShoppingCarts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckedOut",
                table: "ShoppingCarts");
        }
    }
}
