using Microsoft.EntityFrameworkCore.Migrations;

namespace Recepti.Migrations
{
    public partial class Added_stack_trace_to_error_loggin_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StackTrace",
                table: "ErrorLogging",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StackTrace",
                table: "ErrorLogging");
        }
    }
}
