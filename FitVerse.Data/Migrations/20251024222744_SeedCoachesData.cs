using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCoachesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CoachSpecialties",
                columns: new[] { "CoachId", "SpecialtyId", "Certification" },
                values: new object[,]
                {
                    { "C1", 1, "" },
                    { "C2", 2, "" },
                    { "C3", 3, "" }
                });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C2",
                columns: new[] { "About", "ExperienceYears" },
                values: new object[] { "Cardio and endurance specialist with personalized HIIT plans.", 6 });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C3",
                column: "About",
                value: "Yoga and mobility instructor focused on flexibility and wellness.");

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "About", "ExperienceYears", "ImagePath", "IsActive", "Name", "UserId" },
                values: new object[,]
                {
                    { "C4", "CrossFit certified coach delivering high-intensity programs.", 5, "/images/coaches/chris.jpg", true, "Chris Evans", null },
                    { "C5", "Boxing and MMA trainer with focus on endurance and strength.", 4, "/images/coaches/amanda.jpg", true, "Amanda Davis", null },
                    { "C6", "Professional bodybuilder and muscle growth expert.", 10, "/images/coaches/robert.jpg", true, "Robert Wilson", null },
                    { "C7", "Running and endurance coach with marathon training expertise.", 5, "/images/coaches/emily.jpg", true, "Emily Clark", null },
                    { "C8", "Nutrition and weight loss expert with balanced diet programs.", 6, "/images/coaches/david.jpg", true, "David Harris", null }
                });

            migrationBuilder.InsertData(
                table: "CoachSpecialties",
                columns: new[] { "CoachId", "SpecialtyId", "Certification" },
                values: new object[,]
                {
                    { "C4", 4, "" },
                    { "C5", 5, "" },
                    { "C6", 6, "" },
                    { "C7", 7, "" },
                    { "C8", 8, "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CoachSpecialties",
                keyColumns: new[] { "CoachId", "SpecialtyId" },
                keyValues: new object[] { "C1", 1 });

            migrationBuilder.DeleteData(
                table: "CoachSpecialties",
                keyColumns: new[] { "CoachId", "SpecialtyId" },
                keyValues: new object[] { "C2", 2 });

            migrationBuilder.DeleteData(
                table: "CoachSpecialties",
                keyColumns: new[] { "CoachId", "SpecialtyId" },
                keyValues: new object[] { "C3", 3 });

            migrationBuilder.DeleteData(
                table: "CoachSpecialties",
                keyColumns: new[] { "CoachId", "SpecialtyId" },
                keyValues: new object[] { "C4", 4 });

            migrationBuilder.DeleteData(
                table: "CoachSpecialties",
                keyColumns: new[] { "CoachId", "SpecialtyId" },
                keyValues: new object[] { "C5", 5 });

            migrationBuilder.DeleteData(
                table: "CoachSpecialties",
                keyColumns: new[] { "CoachId", "SpecialtyId" },
                keyValues: new object[] { "C6", 6 });

            migrationBuilder.DeleteData(
                table: "CoachSpecialties",
                keyColumns: new[] { "CoachId", "SpecialtyId" },
                keyValues: new object[] { "C7", 7 });

            migrationBuilder.DeleteData(
                table: "CoachSpecialties",
                keyColumns: new[] { "CoachId", "SpecialtyId" },
                keyValues: new object[] { "C8", 8 });

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C4");

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C5");

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C6");

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C7");

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C8");

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C2",
                columns: new[] { "About", "ExperienceYears" },
                values: new object[] { "Specialist in Cardio and Endurance training", 5 });

            migrationBuilder.UpdateData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C3",
                column: "About",
                value: "Yoga and Flexibility Coach");
        }
    }
}
