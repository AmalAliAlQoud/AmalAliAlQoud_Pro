using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicApplication.Migrations
{
    /// <inheritdoc />
    public partial class addFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Patients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Medical_Records",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_Uuid",
                table: "Prescriptions",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Uuid",
                table: "Patients",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medical_Records_Uuid",
                table: "Medical_Records",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Uuid",
                table: "Doctors",
                column: "Uuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Doctor_ID",
                table: "Appointments",
                column: "Doctor_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_Uuid",
                table: "Appointments",
                column: "Uuid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_Doctor_ID",
                table: "Appointments",
                column: "Doctor_ID",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_Doctor_ID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_Uuid",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Uuid",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Medical_Records_Uuid",
                table: "Medical_Records");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_Uuid",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Doctor_ID",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_Uuid",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Medical_Records",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Uuid",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
