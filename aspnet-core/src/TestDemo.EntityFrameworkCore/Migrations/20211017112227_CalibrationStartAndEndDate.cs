using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class CalibrationStartAndEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Serial",
                table: "CalibrationHistory_Comm_Cons_PD");

            migrationBuilder.AlterColumn<int>(
                name: "Current_Rating",
                table: "TrackCalibrationPdCommsConsException",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CalibrationRunPdCrDrs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CalibrationRunPdCrDrs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CalibrationRunLgdRecoveryRate",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CalibrationRunLgdRecoveryRate",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CalibrationRunLgdHairCut",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CalibrationRunLgdHairCut",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CalibrationRunEadCcfSummary",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CalibrationRunEadCcfSummary",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CalibrationRunEadBehaviouralTerms",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CalibrationRunEadBehaviouralTerms",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "CalibrationRunCommConsPD",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "CalibrationRunCommConsPD",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Current_Rating",
                table: "CalibrationInput_Comm_Cons_PD",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Current_Rating",
                table: "CalibrationHistory_Comm_Cons_PD",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CalibrationRunPdCrDrs");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CalibrationRunPdCrDrs");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CalibrationRunLgdRecoveryRate");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CalibrationRunLgdRecoveryRate");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CalibrationRunLgdHairCut");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CalibrationRunLgdHairCut");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CalibrationRunEadCcfSummary");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CalibrationRunEadCcfSummary");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CalibrationRunEadBehaviouralTerms");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CalibrationRunEadBehaviouralTerms");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CalibrationRunCommConsPD");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CalibrationRunCommConsPD");

            migrationBuilder.AlterColumn<string>(
                name: "Current_Rating",
                table: "TrackCalibrationPdCommsConsException",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Current_Rating",
                table: "CalibrationInput_Comm_Cons_PD",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Current_Rating",
                table: "CalibrationHistory_Comm_Cons_PD",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "CalibrationHistory_Comm_Cons_PD",
                nullable: false,
                defaultValue: 0);
        }
    }
}
