using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_ReceivablesInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceivablesInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReportingDate = table.Column<DateTime>(nullable: false),
                    ScenarioOptimistic = table.Column<double>(nullable: false),
                    LossDefinition = table.Column<int>(nullable: false),
                    LossRate = table.Column<double>(nullable: false),
                    FLIOverlay = table.Column<bool>(nullable: false),
                    OverlayOptimistic = table.Column<double>(nullable: false),
                    OverlayBase = table.Column<double>(nullable: false),
                    OverlayDownturn = table.Column<double>(nullable: false),
                    InterceptCoefficient = table.Column<double>(nullable: false),
                    IndexCoefficient = table.Column<double>(nullable: false),
                    LossRateCoefficient = table.Column<double>(nullable: false),
                    RegisterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivablesInputs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceivablesInputs");
        }
    }
}
