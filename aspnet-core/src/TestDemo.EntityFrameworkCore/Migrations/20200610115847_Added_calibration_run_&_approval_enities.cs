using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_calibration_run__approval_enities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Current_Rating",
                table: "CalibrationInput_PD_CR_DR",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CalibrationRunEadBehaviouralTerms",
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
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CloseByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationRunEadBehaviouralTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationRunEadBehaviouralTerms_AbpUsers_CloseByUserId",
                        column: x => x.CloseByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationRunEadCcfSummary",
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
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CloseByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationRunEadCcfSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationRunEadCcfSummary_AbpUsers_CloseByUserId",
                        column: x => x.CloseByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationRunLgdHairCut",
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
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CloseByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationRunLgdHairCut", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationRunLgdHairCut_AbpUsers_CloseByUserId",
                        column: x => x.CloseByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationRunLgdRecoveryRate",
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
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CloseByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationRunLgdRecoveryRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationRunLgdRecoveryRate_AbpUsers_CloseByUserId",
                        column: x => x.CloseByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationRunPdCrDrs",
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
                    ClosedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CloseByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationRunPdCrDrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationRunPdCrDrs_AbpUsers_CloseByUserId",
                        column: x => x.CloseByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationEadBehaviouralTermApprovals",
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
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationEadBehaviouralTermApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationEadBehaviouralTermApprovals_CalibrationRunEadBehaviouralTerms_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "CalibrationRunEadBehaviouralTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalibrationEadBehaviouralTermApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationEadCcfSummaryApprovals",
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
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationEadCcfSummaryApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationEadCcfSummaryApprovals_CalibrationRunEadCcfSummary_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "CalibrationRunEadCcfSummary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalibrationEadCcfSummaryApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationLgdHairCutApprovals",
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
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationLgdHairCutApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationLgdHairCutApprovals_CalibrationRunLgdHairCut_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "CalibrationRunLgdHairCut",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalibrationLgdHairCutApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationLgdRecoveryRateApprovals",
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
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationLgdRecoveryRateApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationLgdRecoveryRateApprovals_CalibrationRunLgdRecoveryRate_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "CalibrationRunLgdRecoveryRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalibrationLgdRecoveryRateApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CalibrationPdCrDrApprovals",
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
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationPdCrDrApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationPdCrDrApprovals_CalibrationRunPdCrDrs_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "CalibrationRunPdCrDrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CalibrationPdCrDrApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationEadBehaviouralTermApprovals_CalibrationId",
                table: "CalibrationEadBehaviouralTermApprovals",
                column: "CalibrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationEadBehaviouralTermApprovals_ReviewedByUserId",
                table: "CalibrationEadBehaviouralTermApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationEadCcfSummaryApprovals_CalibrationId",
                table: "CalibrationEadCcfSummaryApprovals",
                column: "CalibrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationEadCcfSummaryApprovals_ReviewedByUserId",
                table: "CalibrationEadCcfSummaryApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationLgdHairCutApprovals_CalibrationId",
                table: "CalibrationLgdHairCutApprovals",
                column: "CalibrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationLgdHairCutApprovals_ReviewedByUserId",
                table: "CalibrationLgdHairCutApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationLgdRecoveryRateApprovals_CalibrationId",
                table: "CalibrationLgdRecoveryRateApprovals",
                column: "CalibrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationLgdRecoveryRateApprovals_ReviewedByUserId",
                table: "CalibrationLgdRecoveryRateApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationPdCrDrApprovals_CalibrationId",
                table: "CalibrationPdCrDrApprovals",
                column: "CalibrationId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationPdCrDrApprovals_ReviewedByUserId",
                table: "CalibrationPdCrDrApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationRunEadBehaviouralTerms_CloseByUserId",
                table: "CalibrationRunEadBehaviouralTerms",
                column: "CloseByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationRunEadCcfSummary_CloseByUserId",
                table: "CalibrationRunEadCcfSummary",
                column: "CloseByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationRunLgdHairCut_CloseByUserId",
                table: "CalibrationRunLgdHairCut",
                column: "CloseByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationRunLgdRecoveryRate_CloseByUserId",
                table: "CalibrationRunLgdRecoveryRate",
                column: "CloseByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationRunPdCrDrs_CloseByUserId",
                table: "CalibrationRunPdCrDrs",
                column: "CloseByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalibrationEadBehaviouralTermApprovals");

            migrationBuilder.DropTable(
                name: "CalibrationEadCcfSummaryApprovals");

            migrationBuilder.DropTable(
                name: "CalibrationLgdHairCutApprovals");

            migrationBuilder.DropTable(
                name: "CalibrationLgdRecoveryRateApprovals");

            migrationBuilder.DropTable(
                name: "CalibrationPdCrDrApprovals");

            migrationBuilder.DropTable(
                name: "CalibrationRunEadBehaviouralTerms");

            migrationBuilder.DropTable(
                name: "CalibrationRunEadCcfSummary");

            migrationBuilder.DropTable(
                name: "CalibrationRunLgdHairCut");

            migrationBuilder.DropTable(
                name: "CalibrationRunLgdRecoveryRate");

            migrationBuilder.DropTable(
                name: "CalibrationRunPdCrDrs");

            migrationBuilder.DropColumn(
                name: "Current_Rating",
                table: "CalibrationInput_PD_CR_DR");
        }
    }
}
