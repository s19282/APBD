using Microsoft.EntityFrameworkCore.Migrations;

namespace AdvertApi.Migrations
{
    public partial class AddedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientIdClient",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FBulidlingIdBuilding",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FromIdBuilding",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdClient",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TBulidlingIdBuilding",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToIdBuilding",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CampaignIdCampaign",
                table: "Banners",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCampaign",
                table: "Banners",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_ClientIdClient",
                table: "Campaigns",
                column: "ClientIdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_FBulidlingIdBuilding",
                table: "Campaigns",
                column: "FBulidlingIdBuilding");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_TBulidlingIdBuilding",
                table: "Campaigns",
                column: "TBulidlingIdBuilding");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_CampaignIdCampaign",
                table: "Banners",
                column: "CampaignIdCampaign");

            migrationBuilder.AddForeignKey(
                name: "FK_Banners_Campaigns_CampaignIdCampaign",
                table: "Banners",
                column: "CampaignIdCampaign",
                principalTable: "Campaigns",
                principalColumn: "IdCampaign",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Clients_ClientIdClient",
                table: "Campaigns",
                column: "ClientIdClient",
                principalTable: "Clients",
                principalColumn: "IdClient",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Buildings_FBulidlingIdBuilding",
                table: "Campaigns",
                column: "FBulidlingIdBuilding",
                principalTable: "Buildings",
                principalColumn: "IdBuilding",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Buildings_TBulidlingIdBuilding",
                table: "Campaigns",
                column: "TBulidlingIdBuilding",
                principalTable: "Buildings",
                principalColumn: "IdBuilding",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banners_Campaigns_CampaignIdCampaign",
                table: "Banners");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Clients_ClientIdClient",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Buildings_FBulidlingIdBuilding",
                table: "Campaigns");

            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Buildings_TBulidlingIdBuilding",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_ClientIdClient",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_FBulidlingIdBuilding",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_TBulidlingIdBuilding",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Banners_CampaignIdCampaign",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "ClientIdClient",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "FBulidlingIdBuilding",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "FromIdBuilding",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "IdClient",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "TBulidlingIdBuilding",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "ToIdBuilding",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "CampaignIdCampaign",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "IdCampaign",
                table: "Banners");
        }
    }
}
