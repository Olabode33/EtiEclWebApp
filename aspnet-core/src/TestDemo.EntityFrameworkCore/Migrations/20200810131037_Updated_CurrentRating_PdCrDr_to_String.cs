using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_CurrentRating_PdCrDr_to_String : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Current_Rating",
                table: "TrackCalibrationPdCrDrException",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Current_Rating",
                table: "CalibrationInput_PD_CR_DR",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Current_Rating",
                table: "CalibrationHistory_PD_CR_DR",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Current_Rating",
                table: "TrackCalibrationPdCrDrException",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Current_Rating",
                table: "CalibrationInput_PD_CR_DR",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Current_Rating",
                table: "CalibrationHistory_PD_CR_DR",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
