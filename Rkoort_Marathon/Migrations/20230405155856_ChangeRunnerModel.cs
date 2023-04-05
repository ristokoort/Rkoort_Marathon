using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rkoort_Marathon.Migrations
{
    public partial class ChangeRunnerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Break1_time1",
                table: "runners");

            migrationBuilder.DropColumn(
                name: "Break1_time2",
                table: "runners");

            migrationBuilder.RenameColumn(
                name: "Break2_time2",
                table: "runners",
                newName: "Break2");

            migrationBuilder.RenameColumn(
                name: "Break2_time1",
                table: "runners",
                newName: "Break1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Break2",
                table: "Award",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Break1",
                table: "Award",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Break2",
                table: "runners",
                newName: "Break2_time2");

            migrationBuilder.RenameColumn(
                name: "Break1",
                table: "runners",
                newName: "Break2_time1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Break1_time1",
                table: "runners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Break1_time2",
                table: "runners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Break2",
                table: "Award",
                type: "int",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Break1",
                table: "Award",
                type: "int",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
