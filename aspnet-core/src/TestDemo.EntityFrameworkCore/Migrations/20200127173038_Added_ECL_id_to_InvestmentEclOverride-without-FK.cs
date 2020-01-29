using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_ECL_id_to_InvestmentEclOverridewithoutFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestmentEclOverrides_InvestmentEcls_EclId",
                table: "InvestmentEclOverrides");

            migrationBuilder.DropIndex(
                name: "IX_InvestmentEclOverrides_EclId",
                table: "InvestmentEclOverrides");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclOverrides_EclId",
                table: "InvestmentEclOverrides",
                column: "EclId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestmentEclOverrides_InvestmentEcls_EclId",
                table: "InvestmentEclOverrides",
                column: "EclId",
                principalTable: "InvestmentEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
