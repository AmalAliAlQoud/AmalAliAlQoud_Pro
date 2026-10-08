using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicApplication.Migrations
{
    /// <inheritdoc />
    public partial class addMedicalRecordFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Medical_Records_Doctor_ID",
                table: "Medical_Records",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Records_Patient_ID",
                table: "Medical_Records",
                column: "Patient_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Medical_Records_Doctors_Doctor_ID",
                table: "Medical_Records",
                column: "Doctor_ID",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medical_Records_Patients_Patient_ID",
                table: "Medical_Records",
                column: "Patient_ID",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medical_Records_Doctors_Doctor_ID",
                table: "Medical_Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Medical_Records_Patients_Patient_ID",
                table: "Medical_Records");

            migrationBuilder.DropIndex(
                name: "IX_Medical_Records_Doctor_ID",
                table: "Medical_Records");

            migrationBuilder.DropIndex(
                name: "IX_Medical_Records_Patient_ID",
                table: "Medical_Records");
        }
    }
}
