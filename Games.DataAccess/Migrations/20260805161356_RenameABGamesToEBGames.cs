using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Games.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameABGamesToEBGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceABGames",
                table: "Games",
                newName: "PriceEBGames");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceEBGames",
                table: "Games",
                newName: "PriceABGames");
        }
    }
}
