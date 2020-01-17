using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedInvestmentEclOverride : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentEclOverrides",
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
                    StageOverride = table.Column<int>(nullable: false),
                    OverrideComment = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    InvestmentEclSicrId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclOverrides_InvestmentEclSicr_InvestmentEclSicrId",
                        column: x => x.InvestmentEclSicrId,
                        principalTable: "InvestmentEclSicr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentEclOverrideApprovals",
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
                    ReviewDate = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    InvestmentEclOverrideId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclOverrideApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclOverrideApprovals_InvestmentEclOverrides_InvestmentEclOverrideId",
                        column: x => x.InvestmentEclOverrideId,
                        principalTable: "InvestmentEclOverrides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvestmentEclOverrideApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclOverrideApprovals_InvestmentEclOverrideId",
                table: "InvestmentEclOverrideApprovals",
                column: "InvestmentEclOverrideId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclOverrideApprovals_ReviewedByUserId",
                table: "InvestmentEclOverrideApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclOverrides_InvestmentEclSicrId",
                table: "InvestmentEclOverrides",
                column: "InvestmentEclSicrId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentEclOverrideApprovals");

            migrationBuilder.DropTable(
                name: "InvestmentEclOverrides");
        }
    }
}
