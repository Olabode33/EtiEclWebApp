using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclResultSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclResultSummaries",
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
                    SummaryType = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PreOverrideExposure = table.Column<double>(nullable: true),
                    PreOverrideImpairment = table.Column<double>(nullable: true),
                    PreOverrideCoverageRatio = table.Column<double>(nullable: true),
                    PostOverrideExposure = table.Column<double>(nullable: true),
                    PostOverrideImpairment = table.Column<double>(nullable: true),
                    PostOverrideCoverageRatio = table.Column<double>(nullable: true),
                    WholesaleEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclResultSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclResultSummaries_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclResultSummaries_TenantId",
                table: "WholesaleEclResultSummaries",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclResultSummaries_WholesaleEclId",
                table: "WholesaleEclResultSummaries",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclResultSummaries");
        }
    }
}
