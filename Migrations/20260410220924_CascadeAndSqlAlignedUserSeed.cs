using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Migrations
{
    /// <inheritdoc />
    public partial class CascadeAndSqlAlignedUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 7,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 8,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 10,
                column: "IsAdmin",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 7,
                column: "IsAdmin",
                value: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 8,
                column: "IsAdmin",
                value: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 10,
                column: "IsAdmin",
                value: true);
        }
    }
}
