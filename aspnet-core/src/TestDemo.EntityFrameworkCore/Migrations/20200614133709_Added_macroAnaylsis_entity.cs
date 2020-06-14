using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_macroAnaylsis_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalibrationRunMacroAnalysis",
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
                    ExceptionComment = table.Column<string>(nullable: true),
                    CloseByUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationRunMacroAnalysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalibrationRunMacroAnalysis_AbpUsers_CloseByUserId",
                        column: x => x.CloseByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MacroenonomicData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MacroeconomicId = table.Column<int>(nullable: true),
                    Value = table.Column<double>(nullable: true),
                    Period = table.Column<DateTime>(nullable: true),
                    NPL_Percentage_Ratio = table.Column<double>(nullable: true),
                    AffiliateId = table.Column<long>(nullable: true),
                    MacroId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroenonomicData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MacroResult_CorMat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<double>(nullable: true),
                    MacroEconomicIdA = table.Column<int>(nullable: false),
                    MacroEconomicIdB = table.Column<int>(nullable: false),
                    MacroEconomicLabelA = table.Column<string>(nullable: true),
                    MacroEconomicLabelB = table.Column<string>(nullable: true),
                    MacroId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroResult_CorMat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MacroResult_IndexData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Period = table.Column<string>(nullable: true),
                    Index = table.Column<double>(nullable: true),
                    StandardIndex = table.Column<double>(nullable: true),
                    BfNpl = table.Column<double>(nullable: true),
                    MacroId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroResult_IndexData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MacroResult_PrincipalComponent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PrincipalComponent1 = table.Column<double>(nullable: true),
                    PrincipalComponent2 = table.Column<double>(nullable: true),
                    PrincipalComponent3 = table.Column<double>(nullable: true),
                    PrincipalComponent4 = table.Column<double>(nullable: true),
                    PrincipalComponent5 = table.Column<double>(nullable: true),
                    MacroId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroResult_PrincipalComponent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MacroResult_PrincipalComponentSummary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<double>(nullable: true),
                    PrincipalComponentIdA = table.Column<int>(nullable: false),
                    PrincipalComponentIdB = table.Column<int>(nullable: false),
                    PricipalComponentLabelA = table.Column<string>(nullable: true),
                    PricipalComponentLabelB = table.Column<string>(nullable: true),
                    MacroId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroResult_PrincipalComponentSummary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MacroResult_Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IndexWeight1 = table.Column<double>(nullable: true),
                    IndexWeight2 = table.Column<double>(nullable: true),
                    IndexWeight3 = table.Column<double>(nullable: true),
                    IndexWeight4 = table.Column<double>(nullable: true),
                    IndexWeight5 = table.Column<double>(nullable: true),
                    StandardDev = table.Column<double>(nullable: true),
                    Average = table.Column<double>(nullable: true),
                    Correlation = table.Column<double>(nullable: true),
                    TTC_PD = table.Column<double>(nullable: true),
                    MacroId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroResult_Statistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MacroAnalysisApprovals",
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
                    table.PrimaryKey("PK_MacroAnalysisApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MacroAnalysisApprovals_CalibrationRunMacroAnalysis_CalibrationId",
                        column: x => x.CalibrationId,
                        principalTable: "CalibrationRunMacroAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MacroAnalysisApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalibrationRunMacroAnalysis_CloseByUserId",
                table: "CalibrationRunMacroAnalysis",
                column: "CloseByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MacroAnalysisApprovals_CalibrationId",
                table: "MacroAnalysisApprovals",
                column: "CalibrationId");

            migrationBuilder.CreateIndex(
                name: "IX_MacroAnalysisApprovals_ReviewedByUserId",
                table: "MacroAnalysisApprovals",
                column: "ReviewedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MacroAnalysisApprovals");

            migrationBuilder.DropTable(
                name: "MacroenonomicData");

            migrationBuilder.DropTable(
                name: "MacroResult_CorMat");

            migrationBuilder.DropTable(
                name: "MacroResult_IndexData");

            migrationBuilder.DropTable(
                name: "MacroResult_PrincipalComponent");

            migrationBuilder.DropTable(
                name: "MacroResult_PrincipalComponentSummary");

            migrationBuilder.DropTable(
                name: "MacroResult_Statistics");

            migrationBuilder.DropTable(
                name: "CalibrationRunMacroAnalysis");
        }
    }
}
