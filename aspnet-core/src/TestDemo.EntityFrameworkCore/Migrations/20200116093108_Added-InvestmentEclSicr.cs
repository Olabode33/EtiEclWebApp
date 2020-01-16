using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedInvestmentEclSicr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentEclSicr",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    RecordId = table.Column<Guid>(nullable: false),
                    AssetDescription = table.Column<string>(nullable: true),
                    AssetType = table.Column<string>(nullable: true),
                    SovereignDebt = table.Column<string>(nullable: true),
                    CurrentCreditRating = table.Column<string>(nullable: true),
                    PrudentialClassification = table.Column<string>(nullable: true),
                    ForebearanceFlag = table.Column<string>(nullable: true),
                    FitchRating = table.Column<string>(nullable: true),
                    StageClassification = table.Column<int>(nullable: false),
                    StageForebearance = table.Column<int>(nullable: false),
                    StageRating = table.Column<int>(nullable: false),
                    FinalStage = table.Column<int>(nullable: false),
                    EclId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentEclSicr", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvestmentEclSicr_InvestmentEcls_EclId",
                        column: x => x.EclId,
                        principalTable: "InvestmentEcls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvestmentEclSicr_EclId",
                table: "InvestmentEclSicr",
                column: "EclId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvestmentEclSicr");
        }
    }
}
