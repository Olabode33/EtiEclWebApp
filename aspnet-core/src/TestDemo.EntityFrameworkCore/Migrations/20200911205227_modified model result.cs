using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class modifiedmodelresult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BaseScenarioFinalImpairment",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BaseScenarioIPO",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BaseScenarioOverlay",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BaseScenarioOverrideImpact",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BaseScenarioPreOverlay",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DownturnScenarioExposure",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DownturnScenarioFinalImpairment",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DownturnScenarioIPO",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DownturnScenarioOverlay",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DownturnScenarioOverrideImpact",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DownturnScenarioPreOverlay",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OptimisticScenarioExposure",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OptimisticScenarioFinalImpairment",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OptimisticScenarioIPO",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OptimisticScenarioOverlay",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OptimisticScenarioOverrideImpact",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "OptimisticScenarioPreOverlay",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ResultFinalImpairment",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ResultIPO",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ResultOverlay",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ResultOverrideImpact",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ResultPreOverlay",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ResultsExposure",
                table: "LoanImpairmentModelResults",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseScenarioFinalImpairment",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "BaseScenarioIPO",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "BaseScenarioOverlay",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "BaseScenarioOverrideImpact",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "BaseScenarioPreOverlay",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "DownturnScenarioExposure",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "DownturnScenarioFinalImpairment",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "DownturnScenarioIPO",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "DownturnScenarioOverlay",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "DownturnScenarioOverrideImpact",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "DownturnScenarioPreOverlay",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "OptimisticScenarioExposure",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "OptimisticScenarioFinalImpairment",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "OptimisticScenarioIPO",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "OptimisticScenarioOverlay",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "OptimisticScenarioOverrideImpact",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "OptimisticScenarioPreOverlay",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "ResultFinalImpairment",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "ResultIPO",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "ResultOverlay",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "ResultOverrideImpact",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "ResultPreOverlay",
                table: "LoanImpairmentModelResults");

            migrationBuilder.DropColumn(
                name: "ResultsExposure",
                table: "LoanImpairmentModelResults");
        }
    }
}
