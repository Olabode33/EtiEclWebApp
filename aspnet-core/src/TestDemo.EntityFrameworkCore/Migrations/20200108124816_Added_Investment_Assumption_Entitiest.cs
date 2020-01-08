using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_Investment_Assumption_Entitiest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentEcls",
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
                    ReportingDate = table.Column<DateTime>(nullable: false),
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ClosedByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEcls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEcls_AbpUsers_ClosedByUserId",
                        column: x => x.ClosedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvSecFitchCummulativeDefaultRates",
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
                    Key = table.Column<string>(nullable: true),
                    Rating = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvSecFitchCummulativeDefaultRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvSecMacroEconomicAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    BestValue = table.Column<double>(nullable: false),
                    OptimisticValue = table.Column<double>(nullable: false),
                    DownturnValue = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvSecMacroEconomicAssumptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentEclApprovals",
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
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    ReviewedDate = table.Column<DateTime>(nullable: false),
                    ReviewComment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    InvestmentEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclApprovals_InvestmentEcls_InvestmentEclId",
                        column: x => x.InvestmentEclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvestmentEclApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentEclEadInputAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    EadGroup = table.Column<int>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    InvestmentEclId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclEadInputAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclEadInputAssumptions_InvestmentEcls_InvestmentEclId",
                        column: x => x.InvestmentEclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentEclLgdInputAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    LgdGroup = table.Column<int>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    InvestmentEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclLgdInputAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclLgdInputAssumptions_InvestmentEcls_InvestmentEclId",
                        column: x => x.InvestmentEclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentEclPdFitchDefaultRates",
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
                    Key = table.Column<string>(nullable: true),
                    Rating = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    InvestmentEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclPdFitchDefaultRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclPdFitchDefaultRates_InvestmentEcls_InvestmentEclId",
                        column: x => x.InvestmentEclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentEclPdInputAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    DataType = table.Column<int>(nullable: false),
                    PdGroup = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    InvestmentEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclPdInputAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclPdInputAssumptions_InvestmentEcls_InvestmentEclId",
                        column: x => x.InvestmentEclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvestmentPdInputMacroEconomicAssumptions",
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
                    Key = table.Column<string>(nullable: true),
                    Month = table.Column<int>(nullable: false),
                    BestValue = table.Column<double>(nullable: false),
                    OptimisticValue = table.Column<double>(nullable: false),
                    DownturnValue = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    InvestmentEclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentPdInputMacroEconomicAssumptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentPdInputMacroEconomicAssumptions_InvestmentEcls_InvestmentEclId",
                        column: x => x.InvestmentEclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclApprovals_InvestmentEclId",
                table: "InvestmentEclApprovals",
                column: "InvestmentEclId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclApprovals_ReviewedByUserId",
                table: "InvestmentEclApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclEadInputAssumptions_InvestmentEclId",
                table: "InvestmentEclEadInputAssumptions",
                column: "InvestmentEclId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclLgdInputAssumptions_InvestmentEclId",
                table: "InvestmentEclLgdInputAssumptions",
                column: "InvestmentEclId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclPdFitchDefaultRates_InvestmentEclId",
                table: "InvestmentEclPdFitchDefaultRates",
                column: "InvestmentEclId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclPdInputAssumptions_InvestmentEclId",
                table: "InvestmentEclPdInputAssumptions",
                column: "InvestmentEclId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEcls_ClosedByUserId",
                table: "InvestmentEcls",
                column: "ClosedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentPdInputMacroEconomicAssumptions_InvestmentEclId",
                table: "InvestmentPdInputMacroEconomicAssumptions",
                column: "InvestmentEclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentEclApprovals");

            migrationBuilder.DropTable(
                name: "InvestmentEclEadInputAssumptions");

            migrationBuilder.DropTable(
                name: "InvestmentEclLgdInputAssumptions");

            migrationBuilder.DropTable(
                name: "InvestmentEclPdFitchDefaultRates");

            migrationBuilder.DropTable(
                name: "InvestmentEclPdInputAssumptions");

            migrationBuilder.DropTable(
                name: "InvestmentPdInputMacroEconomicAssumptions");

            migrationBuilder.DropTable(
                name: "InvSecFitchCummulativeDefaultRates");

            migrationBuilder.DropTable(
                name: "InvSecMacroEconomicAssumptions");

            migrationBuilder.DropTable(
                name: "InvestmentEcls");
        }
    }
}
