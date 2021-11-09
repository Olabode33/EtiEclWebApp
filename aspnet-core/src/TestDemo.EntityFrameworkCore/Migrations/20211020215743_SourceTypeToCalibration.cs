using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class SourceTypeToCalibration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DataExportedForCalibration",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "CalibrationHistory_PD_CR_DR",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceType",
                table: "CalibrationHistory_PD_CR_DR",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "CalibrationHistory_LGD_RecoveryRate",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceType",
                table: "CalibrationHistory_LGD_RecoveryRate",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "CalibrationHistory_LGD_HairCut",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceType",
                table: "CalibrationHistory_LGD_HairCut",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "CalibrationHistory_EAD_CCF_Summary",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceType",
                table: "CalibrationHistory_EAD_CCF_Summary",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "CalibrationHistory_EAD_Behavioural_Terms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceType",
                table: "CalibrationHistory_EAD_Behavioural_Terms",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "CalibrationHistory_Comm_Cons_PD",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceType",
                table: "CalibrationHistory_Comm_Cons_PD",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataExportedForCalibration",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "CalibrationHistory_PD_CR_DR");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "CalibrationHistory_PD_CR_DR");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "CalibrationHistory_LGD_RecoveryRate");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "CalibrationHistory_LGD_RecoveryRate");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "CalibrationHistory_LGD_HairCut");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "CalibrationHistory_LGD_HairCut");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "CalibrationHistory_EAD_CCF_Summary");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "CalibrationHistory_EAD_CCF_Summary");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "CalibrationHistory_EAD_Behavioural_Terms");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "CalibrationHistory_EAD_Behavioural_Terms");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "CalibrationHistory_Comm_Cons_PD");

            migrationBuilder.DropColumn(
                name: "SourceType",
                table: "CalibrationHistory_Comm_Cons_PD");
        }
    }
}
