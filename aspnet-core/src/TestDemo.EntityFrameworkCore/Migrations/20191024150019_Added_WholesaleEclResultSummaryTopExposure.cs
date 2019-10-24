using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclResultSummaryTopExposure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclResultSummaryTopExposures",
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
                    TenantId = table.Column<int>(nullable: true),
                    PreOverrideExposure = table.Column<double>(nullable: true),
                    PreOverrideImpairment = table.Column<double>(nullable: true),
                    PreOverrideCoverageRatio = table.Column<double>(nullable: true),
                    PostOverrideExposure = table.Column<double>(nullable: true),
                    PostOverrideImpairment = table.Column<double>(nullable: true),
                    PostOverrideCoverageRatio = table.Column<double>(nullable: true),
                    ContractId = table.Column<string>(nullable: true),
                    WholesaleEclId = table.Column<Guid>(nullable: false),
                    WholesaleEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclResultSummaryTopExposures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclResultSummaryTopExposures_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                        column: x => x.WholesaleEclDataLoanBookId,
                        principalTable: "WholesaleEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WholesaleEclResultSummaryTopExposures_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclResultSummaryTopExposures_TenantId",
                table: "WholesaleEclResultSummaryTopExposures",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclResultSummaryTopExposures_WholesaleEclDataLoanBookId",
                table: "WholesaleEclResultSummaryTopExposures",
                column: "WholesaleEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclResultSummaryTopExposures_WholesaleEclId",
                table: "WholesaleEclResultSummaryTopExposures",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclResultSummaryTopExposures");
        }
    }
}
