using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_Specialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Specialties");

            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "Specialties",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Specialties",
                newName: "Icon");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Specialties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
