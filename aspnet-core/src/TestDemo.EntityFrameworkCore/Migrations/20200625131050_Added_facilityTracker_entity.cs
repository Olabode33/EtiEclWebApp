using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_facilityTracker_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackFacilityStage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Facility = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: true),
                    LastReportingDate = table.Column<DateTime>(nullable: true),
                    Framework = table.Column<int>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackFacilityStage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackFacilityStage");
        }
    }
}
