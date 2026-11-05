using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addfilespecialist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Specialties",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Specialties");
        }
    }
}
