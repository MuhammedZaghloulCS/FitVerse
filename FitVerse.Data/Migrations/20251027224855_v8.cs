using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "DietPlans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "DietPlans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "DietPlans");
        }
    }
}
