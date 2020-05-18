using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Affiliate_Macroeconomics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AffiliateMacroEconomicVariableOffsets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BackwardOffset = table.Column<int>(nullable: false),
                    AffiliateId = table.Column<long>(nullable: false),
                    MacroeconomicVariableId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AffiliateMacroEconomicVariableOffsets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AffiliateMacroEconomicVariableOffsets_AbpOrganizationUnits_AffiliateId",
                        column: x => x.AffiliateId,
                        principalTable: "AbpOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AffiliateMacroEconomicVariableOffsets_MacroeconomicVariables_MacroeconomicVariableId",
                        column: x => x.MacroeconomicVariableId,
                        principalTable: "MacroeconomicVariables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AffiliateMacroEconomicVariableOffsets_AffiliateId",
                table: "AffiliateMacroEconomicVariableOffsets",
                column: "AffiliateId");

            migrationBuilder.CreateIndex(
                name: "IX_AffiliateMacroEconomicVariableOffsets_MacroeconomicVariableId",
                table: "AffiliateMacroEconomicVariableOffsets",
                column: "MacroeconomicVariableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AffiliateMacroEconomicVariableOffsets");
        }
    }
}
