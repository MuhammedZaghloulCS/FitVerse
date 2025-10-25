using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedSpecialties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Color", "Description", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "#007bff", "Building muscle and power", "fa-solid fa-dumbbell", "Strength Training" },
                    { 2, "#007bff", "Cardiovascular fitness", "fa-solid fa-heartbeat", "Cardio & HIIT" },
                    { 3, "#007bff", "Mobility and stretching", "fa-solid fa-person-praying", "Flexibility & Yoga" },
                    { 4, "#007bff", "High-intensity functional training", "fa-solid fa-bolt", "CrossFit" },
                    { 5, "#007bff", "Combat sports training", "fa-solid fa-hand-fist", "Boxing & MMA" },
                    { 6, "#007bff", "Muscle hypertrophy focus", "fa-solid fa-trophy", "Bodybuilding" },
                    { 7, "#007bff", "Distance and stamina", "fa-solid fa-person-running", "Running & Endurance" },
                    { 8, "#007bff", "Fat loss and nutrition", "fa-solid fa-scale-balanced", "Weight Loss" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
