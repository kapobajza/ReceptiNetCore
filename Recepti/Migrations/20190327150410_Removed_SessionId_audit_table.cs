using Microsoft.EntityFrameworkCore.Migrations;

namespace Recepti.Migrations
{
    public partial class Removed_SessionId_audit_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Audit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "Audit",
                nullable: true);
        }
    }
}
