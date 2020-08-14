using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_PdCalibrationInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Segment",
                table: "TrackCalibrationPdCrDrException",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Segment",
                table: "CalibrationInput_PD_CR_DR",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Segment",
                table: "TrackCalibrationPdCrDrException");

            migrationBuilder.DropColumn(
                name: "Segment",
                table: "CalibrationInput_PD_CR_DR");
        }
    }
}
