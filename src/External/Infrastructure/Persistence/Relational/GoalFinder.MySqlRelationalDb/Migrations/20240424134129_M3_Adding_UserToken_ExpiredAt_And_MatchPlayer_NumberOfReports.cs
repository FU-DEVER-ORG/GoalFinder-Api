using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoalFinder.MySqlRelationalDb.Migrations
{
    /// <inheritdoc />
    public partial class M3_Adding_UserToken_ExpiredAt_And_MatchPlayer_NumberOfReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "UserTokens",
                comment: "Contain user token records.")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredAt",
                table: "UserTokens",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfReports",
                table: "MatchPlayers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredAt",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "NumberOfReports",
                table: "MatchPlayers");

            migrationBuilder.AlterTable(
                name: "UserTokens",
                oldComment: "Contain user token records.")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8mb4");
        }
    }
}
