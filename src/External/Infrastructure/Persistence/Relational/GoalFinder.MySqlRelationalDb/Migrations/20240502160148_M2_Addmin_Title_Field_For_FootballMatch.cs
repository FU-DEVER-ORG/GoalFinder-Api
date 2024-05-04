using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalFinder.MySqlRelationalDb.Migrations
{
    /// <inheritdoc />
    public partial class M2_Addmin_Title_Field_For_FootballMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "FootballMatches",
                type: "VARCHAR(100)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "FootballMatches");
        }
    }
}
