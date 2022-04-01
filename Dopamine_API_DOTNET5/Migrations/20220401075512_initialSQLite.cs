using Microsoft.EntityFrameworkCore.Migrations;

namespace Dopamine_API_DOTNET5.Migrations
{
    public partial class initialSQLite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    points = table.Column<long>(type: "INTEGER", nullable: false),
                    pointsToBeAdded = table.Column<int>(type: "INTEGER", nullable: false),
                    BuildingOne = table.Column<int>(type: "INTEGER", nullable: true),
                    BuildingTwo = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PlayerData",
                columns: new[] { "Id", "points", "pointsToBeAdded", "BuildingOne", "BuildingTwo" },
                values: new object[] { 1, 10L, 0, 1, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerData");
        }
    }
}
