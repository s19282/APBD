using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertApi.Migrations
{
    public partial class AddedBuildingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    IdBuilding = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(maxLength: 100, nullable: false),
                    StreetNumber = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Height = table.Column<decimal>(type: "decimal(5, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.IdBuilding);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buildings");
        }
    }
}
