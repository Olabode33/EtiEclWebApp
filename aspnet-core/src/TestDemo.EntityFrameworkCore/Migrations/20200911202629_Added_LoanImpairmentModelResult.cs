using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_LoanImpairmentModelResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanImpairmentResult");

            migrationBuilder.CreateTable(
                name: "LoanImpairmentModelResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    RegisterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanImpairmentModelResults", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanImpairmentModelResults");

            migrationBuilder.CreateTable(
                name: "LoanImpairmentResult",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BaseScenarioExposure = table.Column<double>(nullable: false),
                    BaseScenarioFinalImpairment = table.Column<double>(nullable: false),
                    BaseScenarioIPO = table.Column<double>(nullable: false),
                    BaseScenarioOverlay = table.Column<double>(nullable: false),
                    BaseScenarioOverrideImpact = table.Column<double>(nullable: false),
                    BaseScenarioPreOverlay = table.Column<double>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DownturnScenarioExposure = table.Column<double>(nullable: false),
                    DownturnScenarioFinalImpairment = table.Column<double>(nullable: false),
                    DownturnScenarioIPO = table.Column<double>(nullable: false),
                    DownturnScenarioOverlay = table.Column<double>(nullable: false),
                    DownturnScenarioOverrideImpact = table.Column<double>(nullable: false),
                    DownturnScenarioPreOverlay = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    OptimisticScenarioExposure = table.Column<double>(nullable: false),
                    OptimisticScenarioFinalImpairment = table.Column<double>(nullable: false),
                    OptimisticScenarioIPO = table.Column<double>(nullable: false),
                    OptimisticScenarioOverlay = table.Column<double>(nullable: false),
                    OptimisticScenarioOverrideImpact = table.Column<double>(nullable: false),
                    OptimisticScenarioPreOverlay = table.Column<double>(nullable: false),
                    RegisterId = table.Column<Guid>(nullable: false),
                    ResultFinalImpairment = table.Column<double>(nullable: false),
                    ResultIPO = table.Column<double>(nullable: false),
                    ResultOverlay = table.Column<double>(nullable: false),
                    ResultOverrideImpact = table.Column<double>(nullable: false),
                    ResultPreOverlay = table.Column<double>(nullable: false),
                    ResultsExposure = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanImpairmentResult", x => x.Id);
                });
        }
    }
}
