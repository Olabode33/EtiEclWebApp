using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_overrideapprovaltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclOverrides_ObeEclDataLoanBooks_ObeEclDataLoanBookId",
                table: "ObeEclOverrides");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailEclOverrides_RetailEclDataLoanBooks_RetailEclDataLoanBookId",
                table: "RetailEclOverrides");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclOverrides_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclOverrides_WholesaleEclDataLoanBookId",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropIndex(
                name: "IX_RetailEclOverrides_RetailEclDataLoanBookId",
                table: "RetailEclOverrides");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclOverrides_ObeEclDataLoanBookId",
                table: "ObeEclOverrides");

            migrationBuilder.RenameColumn(
                name: "WholesaleEclDataLoanBookId",
                table: "WholesaleEclOverrides",
                newName: "WholesaleEclId");

            migrationBuilder.RenameColumn(
                name: "RetailEclDataLoanBookId",
                table: "RetailEclOverrides",
                newName: "RetailEclId");

            migrationBuilder.RenameColumn(
                name: "ObeEclDataLoanBookId",
                table: "ObeEclOverrides",
                newName: "ObeEclId");

            migrationBuilder.CreateTable(
                name: "ObeEclOverrideApprovals",
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
                    EclOverrideId = table.Column<Guid>(nullable: true),
                    ReviewDate = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    ObeEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObeEclOverrideApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObeEclOverrideApprovals_ObeEcls_ObeEclId",
                        column: x => x.ObeEclId,
                        principalTable: "ObeEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ObeEclOverrideApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclOverrideApprovals",
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
                    EclOverrideId = table.Column<Guid>(nullable: true),
                    ReviewDate = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    RetailEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclOverrideApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclOverrideApprovals_RetailEcls_RetailEclId",
                        column: x => x.RetailEclId,
                        principalTable: "RetailEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RetailEclOverrideApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WholesaleEclOverrideApprovals",
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
                    EclOverrideId = table.Column<Guid>(nullable: true),
                    ReviewDate = table.Column<DateTime>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    WholesaleEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholesaleEclOverrideApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WholesaleEclOverrideApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WholesaleEclOverrideApprovals_WholesaleEcls_WholesaleEclId",
                        column: x => x.WholesaleEclId,
                        principalTable: "WholesaleEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclOverrideApprovals_ObeEclId",
                table: "ObeEclOverrideApprovals",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclOverrideApprovals_ReviewedByUserId",
                table: "ObeEclOverrideApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclOverrideApprovals_RetailEclId",
                table: "RetailEclOverrideApprovals",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclOverrideApprovals_ReviewedByUserId",
                table: "RetailEclOverrideApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclOverrideApprovals_ReviewedByUserId",
                table: "WholesaleEclOverrideApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclOverrideApprovals_WholesaleEclId",
                table: "WholesaleEclOverrideApprovals",
                column: "WholesaleEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObeEclOverrideApprovals");

            migrationBuilder.DropTable(
                name: "RetailEclOverrideApprovals");

            migrationBuilder.DropTable(
                name: "WholesaleEclOverrideApprovals");

            migrationBuilder.RenameColumn(
                name: "WholesaleEclId",
                table: "WholesaleEclOverrides",
                newName: "WholesaleEclDataLoanBookId");

            migrationBuilder.RenameColumn(
                name: "RetailEclId",
                table: "RetailEclOverrides",
                newName: "RetailEclDataLoanBookId");

            migrationBuilder.RenameColumn(
                name: "ObeEclId",
                table: "ObeEclOverrides",
                newName: "ObeEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclOverrides_WholesaleEclDataLoanBookId",
                table: "WholesaleEclOverrides",
                column: "WholesaleEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclOverrides_RetailEclDataLoanBookId",
                table: "RetailEclOverrides",
                column: "RetailEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclOverrides_ObeEclDataLoanBookId",
                table: "ObeEclOverrides",
                column: "ObeEclDataLoanBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclOverrides_ObeEclDataLoanBooks_ObeEclDataLoanBookId",
                table: "ObeEclOverrides",
                column: "ObeEclDataLoanBookId",
                principalTable: "ObeEclDataLoanBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailEclOverrides_RetailEclDataLoanBooks_RetailEclDataLoanBookId",
                table: "RetailEclOverrides",
                column: "RetailEclDataLoanBookId",
                principalTable: "RetailEclDataLoanBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclOverrides_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                table: "WholesaleEclOverrides",
                column: "WholesaleEclDataLoanBookId",
                principalTable: "WholesaleEclDataLoanBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
