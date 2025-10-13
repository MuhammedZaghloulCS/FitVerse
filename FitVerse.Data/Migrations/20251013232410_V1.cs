using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Anatomies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Upper Body" },
                    { 2, "Lower Body" },
                    { 3, "Core" }
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "About", "ImagePath", "IsActive", "Name", "Title", "UserId", "UserId1" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Experienced trainer specializing in strength and conditioning.", "coach1.jpg", true, "John Smith", "Certified Personal Trainer", new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dumbbell" },
                    { 2, "Barbell" },
                    { 3, "Machine" },
                    { 4, "Bodyweight" }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "Content", "CreatedAt", "IsRead", "ReciverId", "ReciverId1", "RefId", "Type" },
                values: new object[] { 1, "Welcome to FitVerse!", new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("00000000-0000-0000-0000-000000000000"), null, 0, 0 });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Strength Training" },
                    { 2, "Cardio" },
                    { 3, "Nutrition" }
                });

            migrationBuilder.InsertData(
                table: "Muscles",
                columns: new[] { "Id", "AnatomyId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Biceps" },
                    { 2, 1, "Triceps" },
                    { 3, 2, "Quadriceps" },
                    { 4, 3, "Abs" }
                });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CoachId", "Name", "Price", "Sessions" },
                values: new object[,]
                {
                    { 1, new Guid("11111111-1111-1111-1111-111111111111"), "Basic Package", 100.0, 8 },
                    { 2, new Guid("11111111-1111-1111-1111-111111111111"), "Premium Package", 250.0, 20 }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Age", "CoachId", "Gender", "Goal", "Height", "Image", "IsActive", "Name", "PackageId", "StartWeight", "UserId", "UserId1" },
                values: new object[] { new Guid("22222222-2222-2222-2222-222222222222"), 28, new Guid("11111111-1111-1111-1111-111111111111"), "Male", "Lose 10kg", 180.0, "client1.jpg", true, "Ahmed Ali", 1, 85.0, new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Description", "EquipmentId", "MuscleId", "Name", "VideoLink" },
                values: new object[,]
                {
                    { 1, "Perform curls using dumbbells to target biceps.", 1, 1, "Bicep Curl", null },
                    { 2, "Cable exercise for triceps.", 3, 2, "Triceps Pushdown", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Notifications",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 2);

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
                table: "Anatomies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Anatomies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Anatomies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));
        }
    }
}
