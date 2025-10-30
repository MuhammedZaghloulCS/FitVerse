using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitVerse.Data.Migrations
{
    /// <inheritdoc />
    public partial class adddescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
<<<<<<<< HEAD:FitVerse.Data/Migrations/20251027150701_v2.cs
                name: "Name",
                table: "DietPlans",
========
                name: "Description",
                table: "Specialties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Specialties",
>>>>>>>> 9a48eb38976a622d68a07e0287c42302a3fe4d1b:FitVerse.Data/Migrations/20251024192955_add-description.cs
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
<<<<<<<< HEAD:FitVerse.Data/Migrations/20251027150701_v2.cs
                name: "Name",
                table: "DietPlans");
========
                name: "Description",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Specialties");
>>>>>>>> 9a48eb38976a622d68a07e0287c42302a3fe4d1b:FitVerse.Data/Migrations/20251024192955_add-description.cs
        }
    }
}
