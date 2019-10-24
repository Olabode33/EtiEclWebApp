using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_WholesaleEclSicrApproval5131 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclSicrApprovals_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrApprovals");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclSicrApprovals_WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrApprovals");

            migrationBuilder.DropColumn(
                name: "WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrApprovals");

            migrationBuilder.AddColumn<Guid>(
                name: "WholesaleEclSicrId",
                table: "WholesaleEclSicrApprovals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclSicrApprovals_WholesaleEclSicrId",
                table: "WholesaleEclSicrApprovals",
                column: "WholesaleEclSicrId");

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclSicrApprovals_WholesaleEclSicrs_WholesaleEclSicrId",
                table: "WholesaleEclSicrApprovals",
                column: "WholesaleEclSicrId",
                principalTable: "WholesaleEclSicrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclSicrApprovals_WholesaleEclSicrs_WholesaleEclSicrId",
                table: "WholesaleEclSicrApprovals");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclSicrApprovals_WholesaleEclSicrId",
                table: "WholesaleEclSicrApprovals");

            migrationBuilder.DropColumn(
                name: "WholesaleEclSicrId",
                table: "WholesaleEclSicrApprovals");

            migrationBuilder.AddColumn<Guid>(
                name: "WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrApprovals",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclSicrApprovals_WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrApprovals",
                column: "WholesaleEclDataLoanBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclSicrApprovals_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrApprovals",
                column: "WholesaleEclDataLoanBookId",
                principalTable: "WholesaleEclDataLoanBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
