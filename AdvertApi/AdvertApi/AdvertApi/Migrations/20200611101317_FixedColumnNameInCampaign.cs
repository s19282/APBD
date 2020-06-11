using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertApi.Migrations
{
    public partial class FixedColumnNameInCampaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricePreSquareMeter",
                table: "Campaigns");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerSquareMeter",
                table: "Campaigns",
                type: "decimal(5, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Login",
                table: "Clients",
                column: "Login",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clients_Login",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PricePerSquareMeter",
                table: "Campaigns");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePreSquareMeter",
                table: "Campaigns",
                type: "decimal(5, 2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
