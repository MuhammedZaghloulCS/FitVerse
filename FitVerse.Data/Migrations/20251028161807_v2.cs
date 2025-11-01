using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CurrentWeight",
                table: "DailyLogs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "DailyLogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentWeight",
                table: "DailyLogs");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "DailyLogs");
        }
    }
}
