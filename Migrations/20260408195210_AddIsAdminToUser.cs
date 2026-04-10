using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAdminToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 6,
                column: "IsAdmin",
                value: false);

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
                keyValue: 9,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 10,
                column: "IsAdmin",
                value: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 11,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 12,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 13,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 14,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 15,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 16,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 17,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 18,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 19,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 20,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 21,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 22,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 23,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 24,
                column: "IsAdmin",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "User_ID",
                keyValue: 25,
                column: "IsAdmin",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");
        }
    }
}
