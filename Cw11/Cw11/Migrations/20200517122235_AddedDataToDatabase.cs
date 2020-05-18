using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cw11.Migrations
{
    public partial class AddedDataToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "IdDoctor", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "e1@tmp.us", "FN1", "LN1" },
                    { 2, "e2@tmp.us", "FN2", "LN2" },
                    { 3, "e3@tmp.us", "FN3", "LN3" }
                });

            migrationBuilder.InsertData(
                table: "Medicaments",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "DM1", "M1", "T1" },
                    { 2, "DM2", "M2", "T2" },
                    { 3, "DM3", "M3", "T3" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "IdPatient", "BirdthDate", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Local), "FNP1", "LNP1" },
                    { 2, new DateTime(2022, 5, 17, 0, 0, 0, 0, DateTimeKind.Local), "FNP2", "LNP2" },
                    { 3, new DateTime(2024, 5, 17, 0, 0, 0, 0, DateTimeKind.Local), "FNP3", "LNP3" }
                });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 1, new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 5, 31, 0, 0, 0, 0, DateTimeKind.Local), 1, 1 });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 2, new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 5, 31, 0, 0, 0, 0, DateTimeKind.Local), 2, 2 });

            migrationBuilder.InsertData(
                table: "Prescriptions",
                columns: new[] { "IdPrescription", "Date", "DueDate", "IdDoctor", "IdPatient" },
                values: new object[] { 3, new DateTime(2020, 5, 17, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2020, 5, 31, 0, 0, 0, 0, DateTimeKind.Local), 3, 3 });

            migrationBuilder.InsertData(
                table: "Prescriptions_Medicaments",
                columns: new[] { "IdPrescription", "Details", "Dose", "IdMedicament" },
                values: new object[] { 1, "D123", 1, 1 });

            migrationBuilder.InsertData(
                table: "Prescriptions_Medicaments",
                columns: new[] { "IdPrescription", "Details", "Dose", "IdMedicament" },
                values: new object[] { 2, "D12", 11, 2 });

            migrationBuilder.InsertData(
                table: "Prescriptions_Medicaments",
                columns: new[] { "IdPrescription", "Details", "Dose", "IdMedicament" },
                values: new object[] { 3, "D1", 111, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Prescriptions_Medicaments",
                keyColumn: "IdPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescriptions_Medicaments",
                keyColumn: "IdPrescription",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescriptions_Medicaments",
                keyColumn: "IdPrescription",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "IdMedicament",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "IdMedicament",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medicaments",
                keyColumn: "IdMedicament",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prescriptions",
                keyColumn: "IdPrescription",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "IdDoctor",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "IdDoctor",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "IdDoctor",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "IdPatient",
                keyValue: 3);
        }
    }
}
