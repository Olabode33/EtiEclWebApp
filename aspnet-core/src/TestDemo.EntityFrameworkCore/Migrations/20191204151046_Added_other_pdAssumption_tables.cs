using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Added_other_pdAssumption_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PdInputAssumptionMacroeconomicInputs",
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
                    Value = table.Column<double>(nullable: false),
                    MacroEconomicInputGroup = table.Column<int>(nullable: false),
                    IsComputed = table.Column<string>(nullable: true),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Framework = table.Column<int>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdInputAssumptionMacroeconomicInputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PdInputAssumptionNonInternalModels",
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
                    PdGroup = table.Column<string>(nullable: true),
                    MarginalDefaultRate = table.Column<double>(nullable: false),
                    CummulativeSurvival = table.Column<double>(nullable: false),
                    IsComputed = table.Column<bool>(nullable: false),
                    CanAffiliateEdit = table.Column<bool>(nullable: false),
                    RequiresGroupApproval = table.Column<bool>(nullable: false),
                    Framework = table.Column<int>(nullable: false),
                    OrganizationUnitId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PdInputAssumptionNonInternalModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PdInputAssumptionMacroeconomicInputs");

            migrationBuilder.DropTable(
                name: "PdInputAssumptionNonInternalModels");
        }
    }
}
