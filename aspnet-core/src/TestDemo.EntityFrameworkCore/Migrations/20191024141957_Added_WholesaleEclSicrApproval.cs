using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclSicrApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclSicrApprovals",
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
                    ReviewedDate = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    WholesaleEclDataLoanBookId = table.Column<Guid>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclSicrApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclSicrApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WholesaleEclSicrApprovals_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                        column: x => x.WholesaleEclDataLoanBookId,
                        principalTable: "WholesaleEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclSicrApprovals_ReviewedByUserId",
                table: "WholesaleEclSicrApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclSicrApprovals_TenantId",
                table: "WholesaleEclSicrApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclSicrApprovals_WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrApprovals",
                column: "WholesaleEclDataLoanBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclSicrApprovals");
        }
    }
}
