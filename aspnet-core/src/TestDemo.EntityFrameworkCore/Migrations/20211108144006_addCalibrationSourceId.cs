using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class addCalibrationSourceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_PD_CR_DR",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationHistory_PD_CR_DR",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_LGD_RecoveryRate",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationHistory_LGD_RecoveryRate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_LGD_HairCut",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationHistory_LGD_HairCut",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_EAD_CCF_Summary",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationHistory_EAD_CCF_Summary",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_EAD_Behavioural_Terms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_Comm_Cons_PD",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationHistory_Comm_Cons_PD",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_PD_CR_DR");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationHistory_PD_CR_DR");

            migrationBuilder.DropColumn(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_LGD_RecoveryRate");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationHistory_LGD_RecoveryRate");

            migrationBuilder.DropColumn(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_LGD_HairCut");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationHistory_LGD_HairCut");

            migrationBuilder.DropColumn(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_EAD_CCF_Summary");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationHistory_EAD_CCF_Summary");

            migrationBuilder.DropColumn(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_EAD_Behavioural_Terms");

            migrationBuilder.DropColumn(
                name: "CalibrationSourceId",
                table: "CalibrationHistory_Comm_Cons_PD");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationHistory_Comm_Cons_PD");
        }
    }
}
