using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class updated_macroVariables_retails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MacroeconomicGroup",
                table: "RetailEclPdAssumptionMacroeconomicProjections",
                newName: "MacroeconomicVariableId");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicGroup",
                table: "RetailEclPdAssumptionMacroeconomicInputs",
                newName: "MacroeconomicVariableId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RetailEclPdAssumptionNonInteralModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailEclPdAssumptionMacroeconomicInputs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdAssumptionMacroeconomicProjections_MacroeconomicVariableId",
                table: "RetailEclPdAssumptionMacroeconomicProjections",
                column: "MacroeconomicVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdAssumptionMacroeconomicInputs_MacroeconomicVariableId",
                table: "RetailEclPdAssumptionMacroeconomicInputs",
                column: "MacroeconomicVariableId");

            migrationBuilder.AddForeignKey(
                name: "FK_RetailEclPdAssumptionMacroeconomicInputs_MacroeconomicVariables_MacroeconomicVariableId",
                table: "RetailEclPdAssumptionMacroeconomicInputs",
                column: "MacroeconomicVariableId",
                principalTable: "MacroeconomicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailEclPdAssumptionMacroeconomicProjections_MacroeconomicVariables_MacroeconomicVariableId",
                table: "RetailEclPdAssumptionMacroeconomicProjections",
                column: "MacroeconomicVariableId",
                principalTable: "MacroeconomicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RetailEclPdAssumptionMacroeconomicInputs_MacroeconomicVariables_MacroeconomicVariableId",
                table: "RetailEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailEclPdAssumptionMacroeconomicProjections_MacroeconomicVariables_MacroeconomicVariableId",
                table: "RetailEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropIndex(
                name: "IX_RetailEclPdAssumptionMacroeconomicProjections_MacroeconomicVariableId",
                table: "RetailEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropIndex(
                name: "IX_RetailEclPdAssumptionMacroeconomicInputs_MacroeconomicVariableId",
                table: "RetailEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RetailEclPdAssumptionNonInteralModels");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicVariableId",
                table: "RetailEclPdAssumptionMacroeconomicProjections",
                newName: "MacroeconomicGroup");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicVariableId",
                table: "RetailEclPdAssumptionMacroeconomicInputs",
                newName: "MacroeconomicGroup");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "RetailEclPdAssumptionMacroeconomicInputs",
                nullable: true,
                oldClrType: typeof(double));
        }
    }
}
