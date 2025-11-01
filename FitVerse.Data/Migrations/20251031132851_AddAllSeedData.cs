using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAllSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Anatomies",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "/images/anatomy/chest.png", "Chest" },
                    { 2, "/images/anatomy/back.png", "Back" },
                    { 3, "/images/anatomy/legs.png", "Legs" },
                    { 4, "/images/anatomy/arms.png", "Arms" },
                    { 5, "/images/anatomy/shoulders.png", "Shoulders" }
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "About", "ExperienceYears", "ImagePath", "IsActive", "Name", "UserId" },
                values: new object[,]
                {
                    { "C1", "Expert in Strength and Conditioning", 8, "/images/coaches/john.jpg", true, "John Smith", null },
                    { "C2", "Cardio and endurance specialist with personalized HIIT plans.", 6, "/images/coaches/sarah.jpg", true, "Sarah Johnson", null },
                    { "C3", "Yoga and mobility instructor focused on flexibility and wellness.", 7, "/images/coaches/michael.jpg", true, "Michael Lee", null },
                    { "C4", "CrossFit certified coach delivering high-intensity programs.", 5, "/images/coaches/chris.jpg", true, "Chris Evans", null },
                    { "C5", "Boxing and MMA trainer with focus on endurance and strength.", 4, "/images/coaches/amanda.jpg", true, "Amanda Davis", null },
                    { "C6", "Professional bodybuilder and muscle growth expert.", 10, "/images/coaches/robert.jpg", true, "Robert Wilson", null },
                    { "C7", "Running and endurance coach with marathon training expertise.", 5, "/images/coaches/emily.jpg", true, "Emily Clark", null },
                    { "C8", "Nutrition and weight loss expert with balanced diet programs.", 6, "/images/coaches/david.jpg", true, "David Harris", null }
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "/images/equipment/barbell.png", "Barbell" },
                    { 2, "/images/equipment/bodyweight.png", "Bodyweight" },
                    { 3, "/images/equipment/dumbbell.png", "Dumbbell" },
                    { 4, "/images/equipment/machine.png", "Machine" },
                    { 5, "/images/equipment/cable.png", "Cable" }
                });

            migrationBuilder.InsertData(
                table: "Muscles",
                columns: new[] { "Id", "AnatomyId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Main chest muscle responsible for pushing movements.", "Pectoralis Major" },
                    { 2, 1, "Smaller chest muscle beneath pectoralis major.", "Pectoralis Minor" },
                    { 3, 2, "Large back muscle used in pulling actions.", "Latissimus Dorsi" },
                    { 4, 2, "Upper back and neck muscle responsible for posture.", "Trapezius" },
                    { 5, 3, "Front thigh muscle responsible for leg extension.", "Quadriceps" },
                    { 6, 4, "Front upper arm muscle responsible for arm flexion.", "Biceps" },
                    { 7, 5, "Main shoulder muscle responsible for arm rotation.", "Deltoid" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Description", "EquipmentId", "MuscleId", "Name", "VideoLink" },
                values: new object[,]
                {
                    { 1, "Classic chest exercise using a barbell.", 1, 1, "Bench Press", "https://www.youtube.com/watch?v=rT7DgCr-3pg" },
                    { 2, "Bodyweight exercise targeting the chest and triceps.", 2, 1, "Push Ups", "https://www.youtube.com/watch?v=_l3ySVKYVJ8" },
                    { 3, "Upper back exercise using body weight.", 2, 3, "Pull Ups", "https://www.youtube.com/watch?v=eGo4IYlbE5g" },
                    { 4, "Compound movement targeting the back.", 1, 3, "Barbell Rows", "https://www.youtube.com/watch?v=vT2GjY_Umpw" },
                    { 5, "Leg exercise working quads and glutes.", 1, 5, "Squats", "https://www.youtube.com/watch?v=aclHkVaku9U" },
                    { 6, "Isolated arm exercise for biceps.", 1, 6, "Bicep Curls", "https://www.youtube.com/watch?v=ykJmrZ5v0Oo" },
                    { 7, "Overhead press targeting the deltoid.", 1, 7, "Shoulder Press", "https://www.youtube.com/watch?v=B-aVuyhvLHU" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C1");

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C2");

            migrationBuilder.DeleteData(
                table: "Coaches",
                keyColumn: "Id",
                keyValue: "C3");

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

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Exercises",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Muscles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Anatomies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Anatomies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Anatomies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Anatomies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Anatomies",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
