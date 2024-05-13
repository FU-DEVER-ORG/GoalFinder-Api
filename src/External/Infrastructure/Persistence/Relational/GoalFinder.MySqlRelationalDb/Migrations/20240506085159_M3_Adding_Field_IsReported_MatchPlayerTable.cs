using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalFinder.MySqlRelationalDb.Migrations
{
    /// <inheritdoc />
    public partial class M3_Adding_Field_IsReported_MatchPlayerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "MatchPlayers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IsReported", table: "MatchPlayers");
        }
    }
}
