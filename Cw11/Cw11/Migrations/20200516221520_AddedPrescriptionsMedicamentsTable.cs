using Microsoft.EntityFrameworkCore.Migrations;

namespace Cw11.Migrations
{
    public partial class AddedPrescriptionsMedicamentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prescriptions_Medicaments",
                columns: table => new
                {
                    IdPrescription = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMedicament = table.Column<int>(nullable: false),
                    Dose = table.Column<int>(nullable: true),
                    Details = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions_Medicaments", x => x.IdPrescription);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescriptions_Medicaments");
        }
    }
}
