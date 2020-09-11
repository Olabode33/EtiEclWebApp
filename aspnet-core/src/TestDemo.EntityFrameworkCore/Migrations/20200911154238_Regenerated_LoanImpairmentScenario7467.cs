using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_LoanImpairmentScenario7467 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplyOverridesOptimisticScenario",
                table: "LoanImpairmentScenarios",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "ApplyOverridesDownturnScenario",
                table: "LoanImpairmentScenarios",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<string>(
                name: "ApplyOverridesBaseScenario",
                table: "LoanImpairmentScenarios",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "ApplyOverridesOptimisticScenario",
                table: "LoanImpairmentScenarios",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ApplyOverridesDownturnScenario",
                table: "LoanImpairmentScenarios",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "ApplyOverridesBaseScenario",
                table: "LoanImpairmentScenarios",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
