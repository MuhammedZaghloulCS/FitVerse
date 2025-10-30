using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CarbPercentage",
                table: "DietPlans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "FatsPercentage",
                table: "DietPlans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ProteinPercentage",
                table: "DietPlans",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarbPercentage",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "FatsPercentage",
                table: "DietPlans");

            migrationBuilder.DropColumn(
                name: "ProteinPercentage",
                table: "DietPlans");
        }
    }
}
