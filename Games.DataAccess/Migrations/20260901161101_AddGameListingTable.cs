using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Games.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGameListingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamePlatform");

            migrationBuilder.DropColumn(
                name: "PriceAmazon",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PriceEBGames",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "PriceWalmart",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "GameListings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceEBGames = table.Column<double>(type: "float", nullable: false),
                    PriceAmazon = table.Column<double>(type: "float", nullable: false),
                    PriceWalmart = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameListings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameListings_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameListings_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameListings_GameId",
                table: "GameListings",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameListings_PlatformId",
                table: "GameListings",
                column: "PlatformId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameListings");

            migrationBuilder.AddColumn<double>(
                name: "PriceAmazon",
                table: "Games",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceEBGames",
                table: "Games",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceWalmart",
                table: "Games",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "GamePlatform",
                columns: table => new
                {
                    GamesId = table.Column<int>(type: "int", nullable: false),
                    PlatformsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamePlatform", x => new { x.GamesId, x.PlatformsId });
                    table.ForeignKey(
                        name: "FK_GamePlatform_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamePlatform_Platforms_PlatformsId",
                        column: x => x.PlatformsId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PriceAmazon", "PriceEBGames", "PriceWalmart" },
                values: new object[] { 19.0, 16.0, 20.0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PriceAmazon", "PriceEBGames", "PriceWalmart" },
                values: new object[] { 19.0, 16.0, 20.0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PriceAmazon", "PriceEBGames", "PriceWalmart" },
                values: new object[] { 19.0, 16.0, 20.0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PriceAmazon", "PriceEBGames", "PriceWalmart" },
                values: new object[] { 19.0, 16.0, 20.0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PriceAmazon", "PriceEBGames", "PriceWalmart" },
                values: new object[] { 19.0, 16.0, 20.0 });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PriceAmazon", "PriceEBGames", "PriceWalmart" },
                values: new object[] { 19.0, 16.0, 20.0 });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_PlatformsId",
                table: "GamePlatform",
                column: "PlatformsId");
        }
    }
}
