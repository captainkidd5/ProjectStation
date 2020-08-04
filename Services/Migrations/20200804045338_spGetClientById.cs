using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    //FromSqlRaw: Execute a sql query or stored procedure that returns entities, such as a list of clients.

    //ExecuteSqlRaw: Execute a sql query or stored procedure that performs database
    //operations but does not return entities. Example-Insert, Update & Delete

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
