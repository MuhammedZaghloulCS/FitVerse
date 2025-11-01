using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSalaryAndCommisionWithInCoach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Commision",
                table: "Coaches",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "Coaches",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C1",
                columns: new[] { "Commision", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C2",
                columns: new[] { "Commision", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C3",
                columns: new[] { "Commision", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C4",
                columns: new[] { "Commision", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C5",
                columns: new[] { "Commision", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C6",
                columns: new[] { "Commision", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C7",
                columns: new[] { "Commision", "Salary" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C8",
                columns: new[] { "Commision", "Salary" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commision",
                table: "Coaches");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Coaches");
        }
    }
}
