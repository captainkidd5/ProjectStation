using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    public partial class shoppingCartImplementation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Products_ProductId",
                table: "ShoppingCartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Amt",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShoppingCartItems",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    CartId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.CartId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShoppingCartItems");

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "ShoppingCartItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Amt",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartItems",
                table: "ShoppingCartItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Products_ProductId",
                table: "ShoppingCartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
