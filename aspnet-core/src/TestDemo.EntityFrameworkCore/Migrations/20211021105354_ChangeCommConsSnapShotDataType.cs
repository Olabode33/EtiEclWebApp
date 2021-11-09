using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class ChangeCommConsSnapShotDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "Snapshot_Date",
            //    table: "TrackCalibrationPdCommsConsException",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Snapshot_Date",
            //    table: "CalibrationInput_Comm_Cons_PD",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Snapshot_Date",
            //    table: "CalibrationHistory_Comm_Cons_PD",
            //    nullable: true,
            //    oldClrType: typeof(DateTime),
            //    oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Snapshot_Date",
                table: "TrackCalibrationPdCommsConsException",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Snapshot_Date",
                table: "CalibrationInput_Comm_Cons_PD",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Snapshot_Date",
                table: "CalibrationHistory_Comm_Cons_PD",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
