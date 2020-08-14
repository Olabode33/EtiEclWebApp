using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_PdCommConsResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Commercial_CureRate",
                table: "CalibrationResult_PD_12Months_Summary",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Commercial_RedefaultRate",
                table: "CalibrationResult_PD_12Months_Summary",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Consumer_CureRate",
                table: "CalibrationResult_PD_12Months_Summary",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Consumer_RedefaultRate",
                table: "CalibrationResult_PD_12Months_Summary",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Segment",
                table: "CalibrationHistory_PD_CR_DR",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CalibrationResult_PD_CommsCons_MarginalDefaultRate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Month = table.Column<int>(nullable: false),
                    Comm1 = table.Column<double>(nullable: true),
                    Cons1 = table.Column<double>(nullable: true),
                    Comm2 = table.Column<double>(nullable: true),
                    Cons2 = table.Column<double>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResult_PD_CommsCons_MarginalDefaultRate", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalibrationResult_PD_CommsCons_MarginalDefaultRate");

            migrationBuilder.DropColumn(
                name: "Commercial_CureRate",
                table: "CalibrationResult_PD_12Months_Summary");

            migrationBuilder.DropColumn(
                name: "Commercial_RedefaultRate",
                table: "CalibrationResult_PD_12Months_Summary");

            migrationBuilder.DropColumn(
                name: "Consumer_CureRate",
                table: "CalibrationResult_PD_12Months_Summary");

            migrationBuilder.DropColumn(
                name: "Consumer_RedefaultRate",
                table: "CalibrationResult_PD_12Months_Summary");

            migrationBuilder.DropColumn(
                name: "Segment",
                table: "CalibrationHistory_PD_CR_DR");
        }
    }
}
