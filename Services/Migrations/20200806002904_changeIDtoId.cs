using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    public partial class changeIDtoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "NewsSnippets",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "NewsSnippets",
                newName: "ID");
        }
    }
}
