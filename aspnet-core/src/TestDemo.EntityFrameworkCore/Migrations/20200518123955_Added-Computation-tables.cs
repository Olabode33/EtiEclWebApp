using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class AddedComputationtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RetailEclComputedEadResults");

            migrationBuilder.DropTable(
                name: "RetailEclSicrApprovals");

            migrationBuilder.DropTable(
                name: "RetailEclSicrs");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclPdSnPCummulativeDefaultRates_TenantId",
                table: "WholesaleEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclPdAssumption12Months_TenantId",
                table: "WholesaleEclPdAssumption12Months");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclLgdAssumptions_TenantId",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclEadInputAssumptions_TenantId",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclAssumptions_TenantId",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropIndex(
                name: "IX_RetailEclPdSnPCummulativeDefaultRates_TenantId",
                table: "RetailEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropIndex(
                name: "IX_RetailEclPdAssumption12Months_TenantId",
                table: "RetailEclPdAssumption12Months");

            migrationBuilder.DropIndex(
                name: "IX_RetailEclLgdAssumptions_TenantId",
                table: "RetailEclLgdAssumptions");

            migrationBuilder.DropIndex(
                name: "IX_RetailEclEadInputAssumptions_TenantId",
                table: "RetailEclEadInputAssumptions");

            migrationBuilder.DropIndex(
                name: "IX_RetailEclAssumptions_TenantId",
                table: "RetailEclAssumptions");

            migrationBuilder.DropIndex(
                name: "IX_RetailEclAssumptionApprovals_TenantId",
                table: "RetailEclAssumptionApprovals");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclPdSnPCummulativeDefaultRates_TenantId",
                table: "ObeEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclPdAssumption12Months_TenantId",
                table: "ObeEclPdAssumption12Months");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclLgdAssumptions_TenantId",
                table: "ObeEclLgdAssumptions");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclEadInputAssumptions_TenantId",
                table: "ObeEclEadInputAssumptions");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclAssumptions_TenantId",
                table: "ObeEclAssumptions");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclAssumptionApprovals_TenantId",
                table: "ObeEclAssumptionApprovals");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "WholesaleEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "WholesaleEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "FSV_Cash",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_CommercialProperty",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Debenture",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Inventory",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_PlantAndEquipment",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Receivables",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_ResidentialProperty",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Shares",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Vehicle",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "AssumptionId",
                table: "WholesaleEclAssumptionApprovals");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "RetailEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "RetailEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "FSV_Cash",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_CommercialProperty",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Debenture",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Inventory",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_PlantAndEquipment",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Receivables",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_ResidentialProperty",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Shares",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Vehicle",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "RetailEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "RetailEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "RetailEclAssumptions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ObeEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "Statue",
                table: "ObeEclPdAssumptionNplIndexes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ObeEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "FSV_Cash",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_CommercialProperty",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Debenture",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Inventory",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_PlantAndEquipment",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Receivables",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_ResidentialProperty",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Shares",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "FSV_Vehicle",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "OverlaysPercentage",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "Stage",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ObeEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ObeEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ObeEclAssumptions");

            migrationBuilder.RenameColumn(
                name: "RedefaultLifetimePD",
                table: "WholesalePdMappings",
                newName: "RedefaultLifetimePd");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicGroup",
                table: "WholesaleEclPdAssumptionMacroeconomicProjections",
                newName: "MacroeconomicVariableId");

            migrationBuilder.RenameColumn(
                name: "MacroEconomicInputGroup",
                table: "WholesaleEclPdAssumptionMacroeconomicInputs",
                newName: "MacroeconomicVariableId");

            migrationBuilder.RenameColumn(
                name: "TtrYears",
                table: "WholesaleEclOverrides",
                newName: "StageOverride");

            migrationBuilder.RenameColumn(
                name: "OverlaysPercentage",
                table: "WholesaleEclOverrides",
                newName: "ImpairmentOverride");

            migrationBuilder.RenameColumn(
                name: "EIR_GROUP",
                table: "WholesaleEadEirProjections",
                newName: "EIR_Group");

            migrationBuilder.RenameColumn(
                name: "Months",
                table: "WholesaleEadCirProjections",
                newName: "Month");

            migrationBuilder.RenameColumn(
                name: "TtrYears",
                table: "RetailEclOverrides",
                newName: "StageOverride");

            migrationBuilder.RenameColumn(
                name: "OverlaysPercentage",
                table: "RetailEclOverrides",
                newName: "ImpairmentOverride");

            migrationBuilder.RenameColumn(
                name: "Months",
                table: "RetailEadCirProjections",
                newName: "Month");

            migrationBuilder.RenameColumn(
                name: "SHARE_OMV",
                table: "ObeLgdCollateralTypeDatas",
                newName: "SHARES_OMV");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicGroup",
                table: "ObeEclPdAssumptionMacroeconomicProjections",
                newName: "MacroeconomicVariableId");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicGroup",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                newName: "MacroeconomicVariableId");

            migrationBuilder.RenameColumn(
                name: "TtrYears",
                table: "ObeEclOverrides",
                newName: "ImpairmentOverride");

            migrationBuilder.RenameColumn(
                name: "EIR_GROUP",
                table: "ObeEadEirProjections",
                newName: "EIR_Group");

            migrationBuilder.RenameColumn(
                name: "Months",
                table: "ObeEadCirProjections",
                newName: "Month");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "WholesalePdLifetimeOptimistics",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "WholesalePdLifetimeDownturns",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "WholesalePdLifetimeBests",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "PLANT_AND_EQUIPMENT_OMV",
                table: "WholesaleLgdCollateralTypeDatas",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsComputed",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "WholesaleEclPdAssumptionNplIndexes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "WholesaleEclPdAssumptionNonInternalModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WholesaleEclPdAssumptionNonInternalModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "WholesaleEclPdAssumptionMacroeconomicProjections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "WholesaleEclPdAssumptionMacroeconomicInputs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "WholesaleEclPdAssumption12Months",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "WholesaleEclPdAssumption12Months",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsComputed",
                table: "WholesaleEclPdAssumption12Months",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WholesaleEclPdAssumption12Months",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ContractId",
                table: "WholesaleEclOverrides",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "OverrideComment",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WholesaleEclOverrides",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WholesaleEclLgdAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WholesaleEclEadInputAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "WholesaleEclAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "WholesaleEclAssumptionApprovals",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "RetailPdLifetimeOptimistics",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "RetailPdLifetimeDownturns",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "RetailEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "RetailEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsComputed",
                table: "RetailEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RetailEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "RetailEclPdAssumptionNplIndexes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "RetailEclPdAssumptionNonInteralModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "RetailEclPdAssumptionMacroeconomicProjections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresGroupApproval",
                table: "RetailEclPdAssumptionMacroeconomicProjections",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "RetailEclPdAssumptionMacroeconomicInputs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "RetailEclPdAssumption12Months",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "RetailEclPdAssumption12Months",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsComputed",
                table: "RetailEclPdAssumption12Months",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RetailEclPdAssumption12Months",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ContractId",
                table: "RetailEclOverrides",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "OverrideComment",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RetailEclOverrides",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RetailEclLgdAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RetailEclEadInputAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "RetailEclAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ObePdLifetimeOptimistics",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ObePdLifetimeDownturns",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "ObePdLifetimeBests",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "PLANT_AND_EQUIPMENT_OMV",
                table: "ObeLgdCollateralTypeDatas",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "ObeEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "ObeEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsComputed",
                table: "ObeEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ObeEclPdSnPCummulativeDefaultRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "ObeEclPdAssumptionNplIndexes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ObeEclPdAssumptionNplIndexes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "ObeEclPdAssumptionNonInternalModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ObeEclPdAssumptionNonInternalModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "ObeEclPdAssumptionMacroeconomicProjections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ObeEclPdAssumptionMacroeconomicProjections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "CanAffiliateEdit",
                table: "ObeEclPdAssumption12Months",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DataType",
                table: "ObeEclPdAssumption12Months",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsComputed",
                table: "ObeEclPdAssumption12Months",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ObeEclPdAssumption12Months",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ContractId",
                table: "ObeEclOverrides",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "OverrideComment",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StageOverride",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ObeEclOverrides",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ObeEclLgdAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ObeEclEadInputAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ObeEclAssumptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "AbpOrganizationUnits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdAssumptionMacroeconomicProjections_MacroeconomicVariableId",
                table: "WholesaleEclPdAssumptionMacroeconomicProjections",
                column: "MacroeconomicVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdAssumptionMacroeconomicInputs_MacroeconomicVariableId",
                table: "WholesaleEclPdAssumptionMacroeconomicInputs",
                column: "MacroeconomicVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdAssumptionMacroeconomicProjections_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicProjections",
                column: "MacroeconomicVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdAssumptionMacroeconomicInputses_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                column: "MacroeconomicVariableId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicInputses_MacroeconomicVariables_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                column: "MacroeconomicVariableId",
                principalTable: "MacroeconomicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicProjections_MacroeconomicVariables_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicProjections",
                column: "MacroeconomicVariableId",
                principalTable: "MacroeconomicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclPdAssumptionMacroeconomicInputs_MacroeconomicVariables_MacroeconomicVariableId",
                table: "WholesaleEclPdAssumptionMacroeconomicInputs",
                column: "MacroeconomicVariableId",
                principalTable: "MacroeconomicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclPdAssumptionMacroeconomicProjections_MacroeconomicVariables_MacroeconomicVariableId",
                table: "WholesaleEclPdAssumptionMacroeconomicProjections",
                column: "MacroeconomicVariableId",
                principalTable: "MacroeconomicVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicInputses_MacroeconomicVariables_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputses");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclPdAssumptionMacroeconomicProjections_MacroeconomicVariables_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclPdAssumptionMacroeconomicInputs_MacroeconomicVariables_MacroeconomicVariableId",
                table: "WholesaleEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclPdAssumptionMacroeconomicProjections_MacroeconomicVariables_MacroeconomicVariableId",
                table: "WholesaleEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclPdAssumptionMacroeconomicProjections_MacroeconomicVariableId",
                table: "WholesaleEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclPdAssumptionMacroeconomicInputs_MacroeconomicVariableId",
                table: "WholesaleEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclPdAssumptionMacroeconomicProjections_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclPdAssumptionMacroeconomicInputses_MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputses");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "WholesaleEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "WholesaleEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "IsComputed",
                table: "WholesaleEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WholesaleEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "WholesaleEclPdAssumptionNplIndexes");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "WholesaleEclPdAssumptionNonInternalModels");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WholesaleEclPdAssumptionNonInternalModels");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "WholesaleEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "WholesaleEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "WholesaleEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "WholesaleEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "IsComputed",
                table: "WholesaleEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WholesaleEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "OverrideComment",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WholesaleEclOverrides");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WholesaleEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WholesaleEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "WholesaleEclAssumptions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "WholesaleEclAssumptionApprovals");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "RetailEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "RetailEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "IsComputed",
                table: "RetailEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RetailEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "RetailEclPdAssumptionNplIndexes");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "RetailEclPdAssumptionNonInteralModels");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "RetailEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropColumn(
                name: "RequiresGroupApproval",
                table: "RetailEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "RetailEclPdAssumptionMacroeconomicInputs");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "RetailEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "RetailEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "IsComputed",
                table: "RetailEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RetailEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "OverrideComment",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RetailEclOverrides");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RetailEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RetailEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RetailEclAssumptions");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "ObeEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "ObeEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "IsComputed",
                table: "ObeEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ObeEclPdSnPCummulativeDefaultRates");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "ObeEclPdAssumptionNplIndexes");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ObeEclPdAssumptionNplIndexes");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "ObeEclPdAssumptionNonInternalModels");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ObeEclPdAssumptionNonInternalModels");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "ObeEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ObeEclPdAssumptionMacroeconomicProjections");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "ObeEclPdAssumptionMacroeconomicInputses");

            migrationBuilder.DropColumn(
                name: "CanAffiliateEdit",
                table: "ObeEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "DataType",
                table: "ObeEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "IsComputed",
                table: "ObeEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ObeEclPdAssumption12Months");

            migrationBuilder.DropColumn(
                name: "OverrideComment",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "StageOverride",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ObeEclOverrides");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ObeEclLgdAssumptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ObeEclEadInputAssumptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ObeEclAssumptions");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "AbpOrganizationUnits");

            migrationBuilder.RenameColumn(
                name: "RedefaultLifetimePd",
                table: "WholesalePdMappings",
                newName: "RedefaultLifetimePD");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicVariableId",
                table: "WholesaleEclPdAssumptionMacroeconomicProjections",
                newName: "MacroeconomicGroup");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicVariableId",
                table: "WholesaleEclPdAssumptionMacroeconomicInputs",
                newName: "MacroEconomicInputGroup");

            migrationBuilder.RenameColumn(
                name: "StageOverride",
                table: "WholesaleEclOverrides",
                newName: "TtrYears");

            migrationBuilder.RenameColumn(
                name: "ImpairmentOverride",
                table: "WholesaleEclOverrides",
                newName: "OverlaysPercentage");

            migrationBuilder.RenameColumn(
                name: "EIR_Group",
                table: "WholesaleEadEirProjections",
                newName: "EIR_GROUP");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "WholesaleEadCirProjections",
                newName: "Months");

            migrationBuilder.RenameColumn(
                name: "StageOverride",
                table: "RetailEclOverrides",
                newName: "TtrYears");

            migrationBuilder.RenameColumn(
                name: "ImpairmentOverride",
                table: "RetailEclOverrides",
                newName: "OverlaysPercentage");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "RetailEadCirProjections",
                newName: "Months");

            migrationBuilder.RenameColumn(
                name: "SHARES_OMV",
                table: "ObeLgdCollateralTypeDatas",
                newName: "SHARE_OMV");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicProjections",
                newName: "MacroeconomicGroup");

            migrationBuilder.RenameColumn(
                name: "MacroeconomicVariableId",
                table: "ObeEclPdAssumptionMacroeconomicInputses",
                newName: "MacroeconomicGroup");

            migrationBuilder.RenameColumn(
                name: "ImpairmentOverride",
                table: "ObeEclOverrides",
                newName: "TtrYears");

            migrationBuilder.RenameColumn(
                name: "EIR_Group",
                table: "ObeEadEirProjections",
                newName: "EIR_GROUP");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "ObeEadCirProjections",
                newName: "Months");

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdLifetimeOptimistics",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdLifetimeDownturns",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "WholesalePdLifetimeBests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PLANT_AND_EQUIPMENT_OMV",
                table: "WholesaleLgdCollateralTypeDatas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "WholesaleEclPdAssumption12Months",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractId",
                table: "WholesaleEclOverrides",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Cash",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_CommercialProperty",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Debenture",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Inventory",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_PlantAndEquipment",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Receivables",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_ResidentialProperty",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Shares",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Vehicle",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "WholesaleEclOverrides",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "WholesaleEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "WholesaleEclLgdAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "WholesaleEclEadInputAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "WholesaleEclAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AssumptionId",
                table: "WholesaleEclAssumptionApprovals",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdLifetimeOptimistics",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "RetailPdLifetimeDownturns",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "RetailEclPdSnPCummulativeDefaultRates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "RetailEclPdAssumption12Months",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractId",
                table: "RetailEclOverrides",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Cash",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_CommercialProperty",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Debenture",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Inventory",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_PlantAndEquipment",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Receivables",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_ResidentialProperty",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Shares",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Vehicle",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "RetailEclOverrides",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "RetailEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "RetailEclLgdAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "RetailEclEadInputAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "RetailEclAssumptions",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdLifetimeOptimistics",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdLifetimeDownturns",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "ObePdLifetimeBests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "PLANT_AND_EQUIPMENT_OMV",
                table: "ObeLgdCollateralTypeDatas",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ObeEclPdSnPCummulativeDefaultRates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Statue",
                table: "ObeEclPdAssumptionNplIndexes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ObeEclPdAssumption12Months",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContractId",
                table: "ObeEclOverrides",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Cash",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_CommercialProperty",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Debenture",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Inventory",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_PlantAndEquipment",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Receivables",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_ResidentialProperty",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Shares",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FSV_Vehicle",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "OverlaysPercentage",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "ObeEclOverrides",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Stage",
                table: "ObeEclOverrides",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ObeEclLgdAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ObeEclEadInputAssumptions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ObeEclAssumptions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RetailEclComputedEadResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    LifetimeEAD = table.Column<string>(nullable: true),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    RetailEclDataLoanBookId = table.Column<Guid>(nullable: true),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclComputedEadResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclComputedEadResults_RetailEclDataLoanBooks_RetailEclDataLoanBookId",
                        column: x => x.RetailEclDataLoanBookId,
                        principalTable: "RetailEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclSicrs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ComputedSICR = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    OverrideComment = table.Column<string>(nullable: true),
                    OverrideSICR = table.Column<string>(nullable: true),
                    RetailEclDataLoanBookId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclSicrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclSicrs_RetailEclDataLoanBooks_RetailEclDataLoanBookId",
                        column: x => x.RetailEclDataLoanBookId,
                        principalTable: "RetailEclDataLoanBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RetailEclSicrApprovals",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    OrganizationUnitId = table.Column<long>(nullable: false),
                    RetailEclSicrId = table.Column<Guid>(nullable: true),
                    ReviewComment = table.Column<string>(nullable: true),
                    ReviewedByUserId = table.Column<long>(nullable: true),
                    ReviewedDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TenantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RetailEclSicrApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RetailEclSicrApprovals_RetailEclSicrs_RetailEclSicrId",
                        column: x => x.RetailEclSicrId,
                        principalTable: "RetailEclSicrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RetailEclSicrApprovals_AbpUsers_ReviewedByUserId",
                        column: x => x.ReviewedByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdSnPCummulativeDefaultRates_TenantId",
                table: "WholesaleEclPdSnPCummulativeDefaultRates",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclPdAssumption12Months_TenantId",
                table: "WholesaleEclPdAssumption12Months",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclLgdAssumptions_TenantId",
                table: "WholesaleEclLgdAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclEadInputAssumptions_TenantId",
                table: "WholesaleEclEadInputAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclAssumptions_TenantId",
                table: "WholesaleEclAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdSnPCummulativeDefaultRates_TenantId",
                table: "RetailEclPdSnPCummulativeDefaultRates",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclPdAssumption12Months_TenantId",
                table: "RetailEclPdAssumption12Months",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclLgdAssumptions_TenantId",
                table: "RetailEclLgdAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclEadInputAssumptions_TenantId",
                table: "RetailEclEadInputAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclAssumptions_TenantId",
                table: "RetailEclAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclAssumptionApprovals_TenantId",
                table: "RetailEclAssumptionApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdSnPCummulativeDefaultRates_TenantId",
                table: "ObeEclPdSnPCummulativeDefaultRates",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdAssumption12Months_TenantId",
                table: "ObeEclPdAssumption12Months",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclLgdAssumptions_TenantId",
                table: "ObeEclLgdAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclEadInputAssumptions_TenantId",
                table: "ObeEclEadInputAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclAssumptions_TenantId",
                table: "ObeEclAssumptions",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclAssumptionApprovals_TenantId",
                table: "ObeEclAssumptionApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclComputedEadResults_RetailEclDataLoanBookId",
                table: "RetailEclComputedEadResults",
                column: "RetailEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclComputedEadResults_TenantId",
                table: "RetailEclComputedEadResults",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclSicrApprovals_RetailEclSicrId",
                table: "RetailEclSicrApprovals",
                column: "RetailEclSicrId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclSicrApprovals_ReviewedByUserId",
                table: "RetailEclSicrApprovals",
                column: "ReviewedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclSicrApprovals_TenantId",
                table: "RetailEclSicrApprovals",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclSicrs_RetailEclDataLoanBookId",
                table: "RetailEclSicrs",
                column: "RetailEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEclSicrs_TenantId",
                table: "RetailEclSicrs",
                column: "TenantId");
        }
    }
}
