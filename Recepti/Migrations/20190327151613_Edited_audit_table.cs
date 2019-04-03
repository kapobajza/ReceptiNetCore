using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Recepti.Migrations
{
    public partial class Edited_audit_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginStatus",
                table: "Audit");

            migrationBuilder.AddColumn<DateTime>(
                name: "AccessDate",
                table: "Audit",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessDate",
                table: "Audit");

            migrationBuilder.AddColumn<string>(
                name: "LoginStatus",
                table: "Audit",
                nullable: true);
        }
    }
}
