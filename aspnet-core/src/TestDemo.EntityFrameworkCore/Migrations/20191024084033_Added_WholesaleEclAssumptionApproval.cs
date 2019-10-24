using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_WholesaleEclAssumptionApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WholesaleEclAssumptionApprovals",
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
                    AssumptionType = table.Column<int>(nullable: false),
                    AssumptionId = table.Column<Guid>(nullable: false),
                    OldValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    DateReviewed = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    WholesaleEclId = table.Column<Guid>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclAssumptionApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclAssumptionApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WholesaleEclAssumptionApprovals_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclAssumptionApprovals_ReviewedByUserId",
                table: "WholesaleEclAssumptionApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclAssumptionApprovals_WholesaleEclId",
                table: "WholesaleEclAssumptionApprovals",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WholesaleEclAssumptionApprovals");
        }
    }
}
