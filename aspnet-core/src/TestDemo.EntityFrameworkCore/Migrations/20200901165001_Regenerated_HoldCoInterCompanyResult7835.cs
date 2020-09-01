using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Regenerated_HoldCoInterCompanyResult7835 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HoldCoInterCompanyResults",
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
                    RegistrationId = table.Column<Guid>(nullable: false),
                    AssetType = table.Column<string>(nullable: true),
                    AssetDescription = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: false),
                    OutstandingBalance = table.Column<double>(nullable: false),
                    BestEstimate = table.Column<double>(nullable: false),
                    Optimistic = table.Column<double>(nullable: false),
                    Downturn = table.Column<double>(nullable: false),
                    Impairment = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoldCoInterCompanyResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HoldCoResultSummaries",
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
                    BestEstimateExposure = table.Column<double>(nullable: false),
                    OptimisticExposure = table.Column<double>(nullable: false),
                    DownturnExposure = table.Column<double>(nullable: false),
                    BestEstimateTotal = table.Column<double>(nullable: false),
                    OptimisticTotal = table.Column<double>(nullable: false),
                    DownturnTotal = table.Column<string>(nullable: true),
                    BestEstimateImpairmentRatio = table.Column<double>(nullable: false),
                    OptimisticImpairmentRatio = table.Column<double>(nullable: false),
                    DownturnImpairmentRatio = table.Column<double>(nullable: false),
                    Exposure = table.Column<double>(nullable: false),
                    Total = table.Column<double>(nullable: false),
                    ImpairmentRatio = table.Column<double>(nullable: false),
                    Check = table.Column<bool>(nullable: false),
                    Diff = table.Column<string>(nullable: true),
                    RegistrationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoldCoResultSummaries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HoldCoInterCompanyResults");

            migrationBuilder.DropTable(
                name: "HoldCoResultSummaries");

            migrationBuilder.CreateTable(
                name: "HoldCoResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AssetDescription = table.Column<string>(nullable: true),
                    AssetType = table.Column<string>(nullable: true),
                    BestEstimate = table.Column<double>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Downturn = table.Column<double>(nullable: false),
                    Impairment = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Optimistic = table.Column<double>(nullable: true),
                    OutstandingBalance = table.Column<double>(nullable: true),
                    Stage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoldCoResults", x => x.Id);
                });
        }
    }
}
