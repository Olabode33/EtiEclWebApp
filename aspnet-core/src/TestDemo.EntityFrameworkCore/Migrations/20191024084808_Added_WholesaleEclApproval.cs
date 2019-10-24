using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclApprovals",
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
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    WholesaleEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WholesaleEclApprovals_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclApprovals_ReviewedByUserId",
                table: "WholesaleEclApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclApprovals_TenantId",
                table: "WholesaleEclApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclApprovals_WholesaleEclId",
                table: "WholesaleEclApprovals",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclApprovals");
        }
    }
}
