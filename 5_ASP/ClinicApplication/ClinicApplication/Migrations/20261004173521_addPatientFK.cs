using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicApplication.Migrations
{
    /// <inheritdoc />
    public partial class addPatientFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Patient_ID",
                table: "Appointments",
                column: "Patient_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_Patient_ID",
                table: "Appointments",
                column: "Patient_ID",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_Patient_ID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Patient_ID",
                table: "Appointments");
        }
    }
}
