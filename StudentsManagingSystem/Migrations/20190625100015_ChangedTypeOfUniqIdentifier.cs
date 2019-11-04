using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsManagingSystem.Migrations
{
    public partial class ChangedTypeOfUniqIdentifier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqNumber",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "UniqId",
                table: "Students",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "UniqNumber",
                table: "Students",
                nullable: false,
                defaultValue: 0);
        }
    }
}
