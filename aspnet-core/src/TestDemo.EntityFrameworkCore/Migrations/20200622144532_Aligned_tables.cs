using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Aligned_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicInputses_MacroeconomicVariables_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputses");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicInputses_ObeEcls_ObeEclId",
                table: "ObeEclPdAssumptionMacroeconomicInputses");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailEclPdAssumptionNonInteralModels_RetailEcls_RetailEclId",
                table: "RetailEclPdAssumptionNonInteralModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RetailEclPdAssumptionNonInteralModels",
                table: "RetailEclPdAssumptionNonInteralModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeEclPdAssumptionMacroeconomicInputses",
                table: "ObeEclPdAssumptionMacroeconomicInputses");

            migrationBuilder.RenameTable(
                name: "RetailEclPdAssumptionNonInteralModels",
                newName: "RetailEclPdAssumptionNonInternalModels");

            migrationBuilder.RenameTable(
                name: "ObeEclPdAssumptionMacroeconomicInputses",
                newName: "ObeEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.RenameIndex(
                name: "IX_RetailEclPdAssumptionNonInteralModels_RetailEclId",
                table: "RetailEclPdAssumptionNonInternalModels",
                newName: "IX_RetailEclPdAssumptionNonInternalModels_RetailEclId");

            migrationBuilder.RenameIndex(
                name: "IX_ObeEclPdAssumptionMacroeconomicInputses_ObeEclId",
                table: "ObeEclPdAssumptionMacroeconomicInputs",
                newName: "IX_ObeEclPdAssumptionMacroeconomicInputs_ObeEclId");

            migrationBuilder.RenameIndex(
                name: "IX_ObeEclPdAssumptionMacroeconomicInputses_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputs",
                newName: "IX_ObeEclPdAssumptionMacroeconomicInputs_MacroeconomicVariableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RetailEclPdAssumptionNonInternalModels",
                table: "RetailEclPdAssumptionNonInternalModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeEclPdAssumptionMacroeconomicInputs",
                table: "ObeEclPdAssumptionMacroeconomicInputs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicInputs_MacroeconomicVariables_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputs",
                column: "MacroeconomicVariableId",
                principalTable: "MacroeconomicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicInputs_ObeEcls_ObeEclId",
                table: "ObeEclPdAssumptionMacroeconomicInputs",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailEclPdAssumptionNonInternalModels_RetailEcls_RetailEclId",
                table: "RetailEclPdAssumptionNonInternalModels",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicInputs_MacroeconomicVariables_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicInputs_ObeEcls_ObeEclId",
                table: "ObeEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailEclPdAssumptionNonInternalModels_RetailEcls_RetailEclId",
                table: "RetailEclPdAssumptionNonInternalModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RetailEclPdAssumptionNonInternalModels",
                table: "RetailEclPdAssumptionNonInternalModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeEclPdAssumptionMacroeconomicInputs",
                table: "ObeEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.RenameTable(
                name: "RetailEclPdAssumptionNonInternalModels",
                newName: "RetailEclPdAssumptionNonInteralModels");

            migrationBuilder.RenameTable(
                name: "ObeEclPdAssumptionMacroeconomicInputs",
                newName: "ObeEclPdAssumptionMacroeconomicInputses");

            migrationBuilder.RenameIndex(
                name: "IX_RetailEclPdAssumptionNonInternalModels_RetailEclId",
                table: "RetailEclPdAssumptionNonInteralModels",
                newName: "IX_RetailEclPdAssumptionNonInteralModels_RetailEclId");

            migrationBuilder.RenameIndex(
                name: "IX_ObeEclPdAssumptionMacroeconomicInputs_ObeEclId",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                newName: "IX_ObeEclPdAssumptionMacroeconomicInputses_ObeEclId");

            migrationBuilder.RenameIndex(
                name: "IX_ObeEclPdAssumptionMacroeconomicInputs_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                newName: "IX_ObeEclPdAssumptionMacroeconomicInputses_MacroeconomicVariableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RetailEclPdAssumptionNonInteralModels",
                table: "RetailEclPdAssumptionNonInteralModels",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeEclPdAssumptionMacroeconomicInputses",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicInputses_MacroeconomicVariables_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                column: "MacroeconomicVariableId",
                principalTable: "MacroeconomicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicInputses_ObeEcls_ObeEclId",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailEclPdAssumptionNonInteralModels_RetailEcls_RetailEclId",
                table: "RetailEclPdAssumptionNonInteralModels",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
