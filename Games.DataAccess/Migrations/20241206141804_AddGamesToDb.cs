using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Games.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddGamesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Producer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    PriceWalmart = table.Column<double>(type: "float", nullable: false),
                    PriceAmazon = table.Column<double>(type: "float", nullable: false),
                    PriceABGames = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "ListPrice", "PriceABGames", "PriceAmazon", "PriceWalmart", "Producer", "Title" },
                values: new object[,]
                {
                    { 1, "Description Grand Theft Auto V", 22.0, 16.0, 19.0, 20.0, "Producteur GTA V", "GTA V" },
                    { 2, "Description FIFA 21", 22.0, 16.0, 19.0, 20.0, "Producteur FIFA 21", "FIFA 21" },
                    { 3, "Description Call of Duty", 22.0, 16.0, 19.0, 20.0, "Producteur Call of Duty", "Call of Duty" },
                    { 4, "Description Assassin's Creed", 22.0, 16.0, 19.0, 20.0, "Producteur Assassin's Creed", "Assassin's Creed" },
                    { 5, "Description Minecraft", 22.0, 16.0, 19.0, 20.0, "Producteur Minecraft", "Minecraft" },
                    { 6, "Description Fortnite", 22.0, 16.0, 19.0, 20.0, "Producteur Fortnite", "Fortnite" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
