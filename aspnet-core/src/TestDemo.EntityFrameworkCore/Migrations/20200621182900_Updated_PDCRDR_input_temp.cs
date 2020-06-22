using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_PDCRDR_input_temp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RAPP_Date",
                table: "CalibrationInput_PD_CR_DR",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "RAPP_Date",
                table: "CalibrationInput_PD_CR_DR",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
