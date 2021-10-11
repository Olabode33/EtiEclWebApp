using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class nonBehaviuraltermSerial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationInput_PD_CR_DR",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationInput_LGD_RecoveryRate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationInput_LGD_HairCut",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationInput_EAD_CCF_Summary",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationInput_PD_CR_DR");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationInput_LGD_RecoveryRate");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationInput_LGD_HairCut");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationInput_EAD_CCF_Summary");
        }
    }
}
