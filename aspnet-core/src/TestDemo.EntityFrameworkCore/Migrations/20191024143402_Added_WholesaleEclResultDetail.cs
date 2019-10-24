using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclResultDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclResultDetails",
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
                    ContractID = table.Column<string>(nullable: true),
                    AccountNo = table.Column<string>(nullable: true),
                    CustomerNo = table.Column<string>(nullable: true),
                    Segment = table.Column<string>(nullable: true),
                    ProductType = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: true),
                    OutstandingBalance = table.Column<double>(nullable: true),
                    PreOverrideEclBest = table.Column<double>(nullable: true),
                    PreOverrideEclOptimistic = table.Column<double>(nullable: true),
                    PreOverrideEclDownturn = table.Column<double>(nullable: true),
                    OverrideStage = table.Column<int>(nullable: true),
                    OverrideTTRYears = table.Column<double>(nullable: true),
                    OverrideFSV = table.Column<double>(nullable: true),
                    OverrideOverlay = table.Column<double>(nullable: true),
                    PostOverrideEclBest = table.Column<double>(nullable: true),
                    PostOverrideEclOptimistic = table.Column<double>(nullable: true),
                    PostOverrideEclDownturn = table.Column<double>(nullable: true),
                    PreOverrideImpairment = table.Column<double>(nullable: true),
                    PostOverrideImpairment = table.Column<double>(nullable: true),
                    WholesaleEclId = table.Column<Guid>(nullable: true),
                    WholesaleEclDataLoanBookId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclResultDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclResultDetails_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                        column: x => x.WholesaleEclDataLoanBookId,
                        principalTable: "WholesaleEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WholesaleEclResultDetails_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclResultDetails_TenantId",
                table: "WholesaleEclResultDetails",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclResultDetails_WholesaleEclDataLoanBookId",
                table: "WholesaleEclResultDetails",
                column: "WholesaleEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclResultDetails_WholesaleEclId",
                table: "WholesaleEclResultDetails",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclResultDetails");
        }
    }
}
