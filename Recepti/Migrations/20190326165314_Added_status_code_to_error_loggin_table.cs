using Microsoft.EntityFrameworkCore.Migrations;

namespace Recepti.Migrations
{
    public partial class Added_status_code_to_error_loggin_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "ErrorLogging",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "ErrorLogging");
        }
    }
}
