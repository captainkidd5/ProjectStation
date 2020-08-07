using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    public partial class spGetProductById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure spGetProductById
                                @Id int
                                as
                                Begin
	                                Select * from Products
	                                where Id = @Id
                                End";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
