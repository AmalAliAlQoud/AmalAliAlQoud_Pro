using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicApplication.Migrations
{
    /// <inheritdoc />
    public partial class addSpatielisttodoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Doctors");

            migrationBuilder.AddColumn<int>(
                name: "Specialty_ID",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Specialty_ID",
                table: "Doctors",
                column: "Specialty_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specialties_Specialty_ID",
                table: "Doctors",
                column: "Specialty_ID",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specialties_Specialty_ID",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_Specialty_ID",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Specialty_ID",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
