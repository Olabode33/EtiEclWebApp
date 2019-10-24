using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclResultSummaryKeyInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclResultSummaryKeyInputs",
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
                    PDGrouping = table.Column<string>(nullable: true),
                    Exposure = table.Column<double>(nullable: true),
                    Collateral = table.Column<double>(nullable: true),
                    UnsecuredPercentage = table.Column<double>(nullable: true),
                    PercentageOfBook = table.Column<double>(nullable: true),
                    Months6CummulativeBestPDs = table.Column<double>(nullable: true),
                    Months12CummulativeBestPDs = table.Column<double>(nullable: true),
                    Months24CummulativeBestPDs = table.Column<double>(nullable: true),
                    WholesaleEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclResultSummaryKeyInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclResultSummaryKeyInputs_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclResultSummaryKeyInputs_TenantId",
                table: "WholesaleEclResultSummaryKeyInputs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclResultSummaryKeyInputs_WholesaleEclId",
                table: "WholesaleEclResultSummaryKeyInputs",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclResultSummaryKeyInputs");
        }
    }
}
