using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotellerie_Hiba.Migrations
{
    /// <inheritdoc />
    public partial class AjoutPaysHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pays",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "Tunisie");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pays",
                table: "Hotels");
        }
    }
}
