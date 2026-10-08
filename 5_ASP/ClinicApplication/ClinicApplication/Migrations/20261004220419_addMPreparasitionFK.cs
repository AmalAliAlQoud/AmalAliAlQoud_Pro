using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicApplication.Migrations
{
    /// <inheritdoc />
    public partial class addMPreparasitionFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_mdRecord_ID",
                table: "Prescriptions",
                column: "mdRecord_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medical_Records_mdRecord_ID",
                table: "Prescriptions",
                column: "mdRecord_ID",
                principalTable: "Medical_Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medical_Records_mdRecord_ID",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_mdRecord_ID",
                table: "Prescriptions");
        }
    }
}
