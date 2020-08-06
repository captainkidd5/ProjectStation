using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    public partial class spGetNewsSnippetById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure spGetNewsSnippetById
                                @Id int
                                as
                                Begin
	                                Select * from NewsSnippets
	                                where Id = @Id
                                End";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure spGetNewsSnippetById";
            migrationBuilder.Sql(procedure);
        }
    }
}
