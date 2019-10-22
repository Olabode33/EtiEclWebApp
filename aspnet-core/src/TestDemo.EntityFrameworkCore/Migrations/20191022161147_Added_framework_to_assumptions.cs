using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_framework_to_assumptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Framework",
                table: "PdInputSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Framework",
                table: "PdInputAssumption12Months",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Framework",
                table: "LgdAssumptionUnsecuredRecoveries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Framework",
                table: "EadInputAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Framework",
                table: "Assumptions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Framework",
                table: "PdInputSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "Framework",
                table: "PdInputAssumption12Months");

            migrationBuilder.DropColumn(
                name: "Framework",
                table: "LgdAssumptionUnsecuredRecoveries");

            migrationBuilder.DropColumn(
                name: "Framework",
                table: "EadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "Framework",
                table: "Assumptions");
        }
    }
}
