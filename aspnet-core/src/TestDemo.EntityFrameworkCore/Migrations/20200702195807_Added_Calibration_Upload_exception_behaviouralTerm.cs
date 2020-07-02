using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_Calibration_Upload_exception_behaviouralTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackCalibrationBehaviouralTermException",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Customer_Name = table.Column<string>(nullable: true),
                    Snapshot_Date = table.Column<DateTime>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Original_Balance_Lcy = table.Column<double>(nullable: true),
                    Outstanding_Balance_Lcy = table.Column<double>(nullable: true),
                    Outstanding_Balance_Acy = table.Column<double>(nullable: true),
                    Contract_Start_Date = table.Column<DateTime>(nullable: true),
                    Contract_End_Date = table.Column<DateTime>(nullable: true),
                    Restructure_Indicator = table.Column<string>(nullable: true),
                    Restructure_Type = table.Column<string>(nullable: true),
                    Restructure_Start_Date = table.Column<DateTime>(nullable: true),
                    Restructure_End_Date = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackCalibrationBehaviouralTermException", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackCalibrationBehaviouralTermException");
        }
    }
}
