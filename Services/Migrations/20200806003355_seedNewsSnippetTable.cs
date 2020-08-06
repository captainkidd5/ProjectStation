using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Migrations
{
    public partial class seedNewsSnippetTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NewsSnippets",
                columns: new[] { "Id", "Date", "Description", "Title" },
                values: new object[] { 1, "2019.03.18", @"Tokyo International Gallery and Gallery Shimamura is participating in Tokyo International Art Fair 2019 (Roppongi) We cordially invite you to join us at Tokyo International Art Fair 2019. 
 Tokyo International Art Fair 2019 
WHEN: Friday 7th June 2019 : 18.00pm – 21.00pm | Opening – Private View(invitations & tickets only) Saturday 8th June 2019 : 11.00am – 18.00pm | Open to Public(free entrance) 
 LOCATION: BELLE SALLE Roppongi Japan, 〒106 - 0032 Tokyo, 
 Minato, Roppongi, 7 Chome−18−18, 住友不動産六本木通ビル", "Tokyo International Art Fair 2019 June 7-8, 2019" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NewsSnippets",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
