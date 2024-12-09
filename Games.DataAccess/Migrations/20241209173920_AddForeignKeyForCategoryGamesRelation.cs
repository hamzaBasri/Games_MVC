using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Games.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyForCategoryGamesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Games",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                column: "CategoryId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                column: "CategoryId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 3,
                column: "CategoryId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 4,
                column: "CategoryId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 5,
                column: "CategoryId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 6,
                column: "CategoryId",
                value: 6);

            migrationBuilder.CreateIndex(
                name: "IX_Games_CategoryId",
                table: "Games",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Categories_CategoryId",
                table: "Games",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Categories_CategoryId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_CategoryId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Games");
        }
    }
}
