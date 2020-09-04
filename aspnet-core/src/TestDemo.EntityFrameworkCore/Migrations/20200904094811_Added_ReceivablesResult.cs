using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_ReceivablesResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReceivablesResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TotalExposure = table.Column<double>(nullable: false),
                    TotalImpairment = table.Column<double>(nullable: false),
                    AdditionalProvision = table.Column<double>(nullable: false),
                    Coverage = table.Column<double>(nullable: false),
                    OptimisticExposure = table.Column<double>(nullable: false),
                    BaseExposure = table.Column<double>(nullable: false),
                    DownturnExposure = table.Column<double>(nullable: false),
                    ECLTotalExposure = table.Column<double>(nullable: false),
                    OptimisticImpairment = table.Column<double>(nullable: false),
                    BaseImpairment = table.Column<double>(nullable: false),
                    DownturnImpairment = table.Column<double>(nullable: false),
                    ECLTotalImpairment = table.Column<double>(nullable: false),
                    OptimisticCoverageRatio = table.Column<double>(nullable: false),
                    BaseCoverageRatio = table.Column<double>(nullable: false),
                    DownturnCoverageRatio = table.Column<double>(nullable: false),
                    TotalCoverageRatio = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceivablesResults", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReceivablesResults");
        }
    }
}
