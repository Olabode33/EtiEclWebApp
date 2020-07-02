using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_Calibration_Upload_exception : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackCalibrationPdCrDrException",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Customer_No = table.Column<string>(nullable: true),
                    Account_No = table.Column<string>(nullable: true),
                    Contract_No = table.Column<string>(nullable: true),
                    Product_Type = table.Column<string>(nullable: true),
                    Days_Past_Due = table.Column<int>(nullable: true),
                    Classification = table.Column<string>(nullable: true),
                    Outstanding_Balance_Lcy = table.Column<double>(nullable: true),
                    Contract_Start_Date = table.Column<DateTime>(nullable: true),
                    Contract_End_Date = table.Column<DateTime>(nullable: true),
                    RAPP_Date = table.Column<int>(nullable: true),
                    Current_Rating = table.Column<int>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackCalibrationPdCrDrException", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackCalibrationPdCrDrException");
        }
    }
}
