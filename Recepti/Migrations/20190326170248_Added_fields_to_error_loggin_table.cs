using Microsoft.EntityFrameworkCore.Migrations;

namespace Recepti.Migrations
{
    public partial class Added_fields_to_error_loggin_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalBasePath",
                table: "ErrorLogging",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OriginalQueryString",
                table: "ErrorLogging",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalBasePath",
                table: "ErrorLogging");

            migrationBuilder.DropColumn(
                name: "OriginalQueryString",
                table: "ErrorLogging");
        }
    }
}
