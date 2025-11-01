using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDifficultyAndDurationWeeks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationInDays",
                table: "ExercisePlans",
                newName: "DurationWeeks");

            migrationBuilder.AddColumn<string>(
                name: "Difficulty",
                table: "ExercisePlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "ExercisePlans");

            migrationBuilder.RenameColumn(
                name: "DurationWeeks",
                table: "ExercisePlans",
                newName: "DurationInDays");
        }
    }
}
