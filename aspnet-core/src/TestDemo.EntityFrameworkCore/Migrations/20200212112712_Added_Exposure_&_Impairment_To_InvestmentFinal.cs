using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_Exposure__Impairment_To_InvestmentFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Exposure",
                table: "InvestmentEclFinalResult",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Impairment",
                table: "InvestmentEclFinalResult",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exposure",
                table: "InvestmentEclFinalResult");

            migrationBuilder.DropColumn(
                name: "Impairment",
                table: "InvestmentEclFinalResult");
        }
    }
}
