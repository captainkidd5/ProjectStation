using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    public partial class spGetClientById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure spGetClientById
                               @Id int
                               as
                               Begin
	                           Select * from Clients
	                           where Id = @Id
                               End";

            migrationBuilder.Sql(procedure);
            
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        string procedure = @"Drop procedure spGetClientById ";
        migrationBuilder.Sql(procedure);
    }
}
}
