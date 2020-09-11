using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_LoanImpairmentScenario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanImpairmentScenarios",
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
                    RegisterId = table.Column<Guid>(nullable: false),
                    ScenarioOption = table.Column<string>(nullable: true),
                    ApplyOverridesBaseScenario = table.Column<bool>(nullable: false),
                    ApplyOverridesOptimisticScenario = table.Column<bool>(nullable: false),
                    ApplyOverridesDownturnScenario = table.Column<bool>(nullable: false),
                    BestScenarioOverridesValue = table.Column<double>(nullable: false),
                    OptimisticScenarioOverridesValue = table.Column<double>(nullable: false),
                    DownturnScenarioOverridesValue = table.Column<double>(nullable: false),
                    BaseScenario = table.Column<double>(nullable: false),
                    OptimisticScenario = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanImpairmentScenarios", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanImpairmentScenarios");
        }
    }
}
