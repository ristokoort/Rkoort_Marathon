using Microsoft.EntityFrameworkCore.Migrations;

namespace Rkoort_Marathon.Migrations
{
    public partial class AddAwardModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Break1",
                table: "Award",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Break2",
                table: "Award",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Break1",
                table: "Award");

            migrationBuilder.DropColumn(
                name: "Break2",
                table: "Award");
        }
    }
}
