using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_PDCRDR_input : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RAPP_Date",
                table: "CalibrationInput_PD_CR_DR",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RAPP_Date",
                table: "CalibrationInput_PD_CR_DR",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
