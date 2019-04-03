using Microsoft.EntityFrameworkCore.Migrations;

namespace Recepti.Migrations
{
    public partial class Added_unique_username_to_korisnik : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KorisnickoIme",
                table: "Korisnici",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Korisnici_KorisnickoIme",
                table: "Korisnici",
                column: "KorisnickoIme",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Korisnici_KorisnickoIme",
                table: "Korisnici");

            migrationBuilder.AlterColumn<string>(
                name: "KorisnickoIme",
                table: "Korisnici",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
