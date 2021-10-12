using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class BehaviouralTermSerial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationInput_EAD_Behavioural_Terms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationHistory_EAD_Behavioural_Terms",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationInput_EAD_Behavioural_Terms");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationHistory_EAD_Behavioural_Terms");
        }
    }
}
