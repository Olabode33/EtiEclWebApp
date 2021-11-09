using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class calibrationIdToWholesale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CalibrationEadBehaviouralTermId",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CalibrationEadCcfSummaryId",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CalibrationLgdHairCutId",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CalibrationLgdRecoveryRateId",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CalibrationPdCommConsId",
                table: "WholesaleEcls",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CalibrationPdCrDrId",
                table: "WholesaleEcls",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalibrationEadBehaviouralTermId",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "CalibrationEadCcfSummaryId",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "CalibrationLgdHairCutId",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "CalibrationLgdRecoveryRateId",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "CalibrationPdCommConsId",
                table: "WholesaleEcls");

            migrationBuilder.DropColumn(
                name: "CalibrationPdCrDrId",
                table: "WholesaleEcls");
        }
    }
}
