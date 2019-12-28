using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class updated_PdMacro_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MacroeconomicGroup",
                table: "PdInputAssumptionMacroeconomicProjections",
                newName: "MacroeconomicVariableId");

            migrationBuilder.RenameColumn(
                name: "MacroEconomicInputGroup",
                table: "PdInputAssumptionMacroeconomicInputs",
                newName: "MacroeconomicVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_PdInputAssumptionMacroeconomicProjections_MacroeconomicVariableId",
                table: "PdInputAssumptionMacroeconomicProjections",
                column: "MacroeconomicVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_PdInputAssumptionMacroeconomicInputs_MacroeconomicVariableId",
                table: "PdInputAssumptionMacroeconomicInputs",
                column: "MacroeconomicVariableId");

            migrationBuilder.AddForeignKey(
                name: "FK_PdInputAssumptionMacroeconomicInputs_MacroeconomicVariables_MacroeconomicVariableId",
                table: "PdInputAssumptionMacroeconomicInputs",
                column: "MacroeconomicVariableId",
                principalTable: "MacroeconomicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PdInputAssumptionMacroeconomicProjections_MacroeconomicVariables_MacroeconomicVariableId",
                table: "PdInputAssumptionMacroeconomicProjections",
                column: "MacroeconomicVariableId",
                principalTable: "MacroeconomicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PdInputAssumptionMacroeconomicInputs_MacroeconomicVariables_MacroeconomicVariableId",
                table: "PdInputAssumptionMacroeconomicInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_PdInputAssumptionMacroeconomicProjections_MacroeconomicVariables_MacroeconomicVariableId",
                table: "PdInputAssumptionMacroeconomicProjections");

            migrationBuilder.DropIndex(
                name: "IX_PdInputAssumptionMacroeconomicProjections_MacroeconomicVariableId",
                table: "PdInputAssumptionMacroeconomicProjections");

            migrationBuilder.DropIndex(
                name: "IX_PdInputAssumptionMacroeconomicInputs_MacroeconomicVariableId",
                table: "PdInputAssumptionMacroeconomicInputs");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicVariableId",
                table: "PdInputAssumptionMacroeconomicProjections",
                newName: "MacroeconomicGroup");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicVariableId",
                table: "PdInputAssumptionMacroeconomicInputs",
                newName: "MacroEconomicInputGroup");
        }
    }
}
