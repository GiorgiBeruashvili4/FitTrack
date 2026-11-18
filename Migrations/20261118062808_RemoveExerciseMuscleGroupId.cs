using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrack.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExerciseMuscleGroupId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ExerciseMuscleGroups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ExerciseMuscleGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
