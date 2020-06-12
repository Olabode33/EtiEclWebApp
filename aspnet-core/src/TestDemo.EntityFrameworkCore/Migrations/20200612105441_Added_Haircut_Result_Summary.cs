using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_Haircut_Result_Summary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalibrationResult_LGD_HairCut_Summary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Debenture = table.Column<double>(nullable: true),
                    Cash = table.Column<double>(nullable: true),
                    Inventory = table.Column<double>(nullable: true),
                    Plant_And_Equipment = table.Column<double>(nullable: true),
                    Residential_Property = table.Column<double>(nullable: true),
                    Commercial_Property = table.Column<double>(nullable: true),
                    Receivables = table.Column<double>(nullable: true),
                    Shares = table.Column<double>(nullable: true),
                    Vehicle = table.Column<double>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    CalibrationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalibrationResult_LGD_HairCut_Summary", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalibrationResult_LGD_HairCut_Summary");
        }
    }
}
