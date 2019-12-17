using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Modified_PdNplIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NplIndexGroup",
                table: "PdInputAssumptionNplIndexes");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "PdInputAssumptionNplIndexes");

            migrationBuilder.AddColumn<double>(
                name: "Actual",
                table: "PdInputAssumptionNplIndexes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "EtiNplSeries",
                table: "PdInputAssumptionNplIndexes",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Standardised",
                table: "PdInputAssumptionNplIndexes",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actual",
                table: "PdInputAssumptionNplIndexes");

            migrationBuilder.DropColumn(
                name: "EtiNplSeries",
                table: "PdInputAssumptionNplIndexes");

            migrationBuilder.DropColumn(
                name: "Standardised",
                table: "PdInputAssumptionNplIndexes");

            migrationBuilder.AddColumn<int>(
                name: "NplIndexGroup",
                table: "PdInputAssumptionNplIndexes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "PdInputAssumptionNplIndexes",
                nullable: true);
        }
    }
}
