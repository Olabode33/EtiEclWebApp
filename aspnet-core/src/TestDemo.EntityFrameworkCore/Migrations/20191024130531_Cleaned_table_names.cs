using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Cleaned_table_names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclPdAssumption12Monthses_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEclPdAssumption12Monthses");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclPdSnPCummulativeDefaultRateses_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEclPdSnPCummulativeDefaultRateses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WholesaleEclPdSnPCummulativeDefaultRateses",
                table: "WholesaleEclPdSnPCummulativeDefaultRateses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WholesaleEclPdAssumption12Monthses",
                table: "WholesaleEclPdAssumption12Monthses");

            migrationBuilder.RenameTable(
                name: "WholesaleEclPdSnPCummulativeDefaultRateses",
                newName: "WholesaleEclPdSnPCummulativeDefaultRates");

            migrationBuilder.RenameTable(
                name: "WholesaleEclPdAssumption12Monthses",
                newName: "WholesaleEclPdAssumption12Months");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEclPdSnPCummulativeDefaultRateses_WholesaleEclId",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                newName: "IX_WholesaleEclPdSnPCummulativeDefaultRates_WholesaleEclId");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEclPdSnPCummulativeDefaultRateses_TenantId",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                newName: "IX_WholesaleEclPdSnPCummulativeDefaultRates_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEclPdAssumption12Monthses_WholesaleEclId",
                table: "WholesaleEclPdAssumption12Months",
                newName: "IX_WholesaleEclPdAssumption12Months_WholesaleEclId");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEclPdAssumption12Monthses_TenantId",
                table: "WholesaleEclPdAssumption12Months",
                newName: "IX_WholesaleEclPdAssumption12Months_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WholesaleEclPdSnPCummulativeDefaultRates",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WholesaleEclPdAssumption12Months",
                table: "WholesaleEclPdAssumption12Months",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclPdAssumption12Months_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEclPdAssumption12Months",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclPdSnPCummulativeDefaultRates_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclPdAssumption12Months_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEclPdAssumption12Months");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclPdSnPCummulativeDefaultRates_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WholesaleEclPdSnPCummulativeDefaultRates",
                table: "WholesaleEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WholesaleEclPdAssumption12Months",
                table: "WholesaleEclPdAssumption12Months");

            migrationBuilder.RenameTable(
                name: "WholesaleEclPdSnPCummulativeDefaultRates",
                newName: "WholesaleEclPdSnPCummulativeDefaultRateses");

            migrationBuilder.RenameTable(
                name: "WholesaleEclPdAssumption12Months",
                newName: "WholesaleEclPdAssumption12Monthses");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEclPdSnPCummulativeDefaultRates_WholesaleEclId",
                table: "WholesaleEclPdSnPCummulativeDefaultRateses",
                newName: "IX_WholesaleEclPdSnPCummulativeDefaultRateses_WholesaleEclId");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEclPdSnPCummulativeDefaultRates_TenantId",
                table: "WholesaleEclPdSnPCummulativeDefaultRateses",
                newName: "IX_WholesaleEclPdSnPCummulativeDefaultRateses_TenantId");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEclPdAssumption12Months_WholesaleEclId",
                table: "WholesaleEclPdAssumption12Monthses",
                newName: "IX_WholesaleEclPdAssumption12Monthses_WholesaleEclId");

            migrationBuilder.RenameIndex(
                name: "IX_WholesaleEclPdAssumption12Months_TenantId",
                table: "WholesaleEclPdAssumption12Monthses",
                newName: "IX_WholesaleEclPdAssumption12Monthses_TenantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WholesaleEclPdSnPCummulativeDefaultRateses",
                table: "WholesaleEclPdSnPCummulativeDefaultRateses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WholesaleEclPdAssumption12Monthses",
                table: "WholesaleEclPdAssumption12Monthses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclPdAssumption12Monthses_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEclPdAssumption12Monthses",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclPdSnPCummulativeDefaultRateses_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEclPdSnPCummulativeDefaultRateses",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
