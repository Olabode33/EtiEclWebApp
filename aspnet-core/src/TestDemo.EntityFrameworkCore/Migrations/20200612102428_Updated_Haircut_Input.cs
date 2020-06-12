using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_Haircut_Input : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "CalibrationInput_LGD_HairCut",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Snapshot_Date",
                table: "CalibrationInput_EAD_CCF_Summary",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Settlement_Account",
                table: "CalibrationInput_EAD_CCF_Summary",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CalibrationResult_PD_12Months",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<int>(nullable: true),
                    Outstanding_Balance = table.Column<double>(nullable: true),
                    Redefault_Balance = table.Column<double>(nullable: true),
                    Redefaulted_Balance = table.Column<double>(nullable: true),
                    Total_Redefault = table.Column<double>(nullable: true),
                    Months_PDs_12 = table.Column<double>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResult_PD_12Months", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalibrationResult_PD_12Months");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "CalibrationInput_LGD_HairCut");

            migrationBuilder.DropColumn(
                name: "Settlement_Account",
                table: "CalibrationInput_EAD_CCF_Summary");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Snapshot_Date",
                table: "CalibrationInput_EAD_CCF_Summary",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
