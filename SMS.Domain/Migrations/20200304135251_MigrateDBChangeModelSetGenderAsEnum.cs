using Microsoft.EntityFrameworkCore.Migrations;

namespace SMS.Domain.Migrations
{
    public partial class MigrateDBChangeModelSetGenderAsEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Gender",
                table: "Students",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(byte),
                oldMaxLength: 10);
        }
    }
}
