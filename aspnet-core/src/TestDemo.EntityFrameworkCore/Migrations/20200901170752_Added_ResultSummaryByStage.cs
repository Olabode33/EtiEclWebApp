using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_ResultSummaryByStage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResultSummaryByStages",
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
                    StageOneExposure = table.Column<double>(nullable: false),
                    StageTwoExposure = table.Column<double>(nullable: false),
                    StageThreeExposure = table.Column<double>(nullable: false),
                    TotalExposure = table.Column<double>(nullable: false),
                    StageOneImpairment = table.Column<double>(nullable: false),
                    StageTwoImpairment = table.Column<double>(nullable: false),
                    StageThreeImpairment = table.Column<double>(nullable: false),
                    StageOneImpairmentRatio = table.Column<double>(nullable: false),
                    StageTwoImpairmentRatio = table.Column<double>(nullable: false),
                    TotalImpairment = table.Column<double>(nullable: false),
                    StageThreeImpairmentRatio = table.Column<double>(nullable: false),
                    TotalImpairmentRatio = table.Column<double>(nullable: false),
                    RegistrationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultSummaryByStages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResultSummaryByStages");
        }
    }
}
