using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDemo.Migrations
{
    public partial class Updated_computation_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ObeEadCirProjections_ObeEcls_ObeEclId",
                table: "ObeEadCirProjections");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEadEirProjections_ObeEcls_ObeEclId",
                table: "ObeEadEirProjections");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEadInputs_ObeEcls_ObeEclId",
                table: "ObeEadInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclEadLifetimeProjections_ObeEcls_ObeEclId",
                table: "ObeEclEadLifetimeProjections");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclFrameworkFinalOverrides_ObeEcls_ObeEclId",
                table: "ObeEclFrameworkFinalOverrides");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclFrameworkFinals_ObeEcls_ObeEclId",
                table: "ObeEclFrameworkFinals");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclLgdCollateral_ObeEcls_ObeEclId",
                table: "ObeEclLgdCollateral");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclLgdCollateralProjection_ObeEcls_ObeEclId",
                table: "ObeEclLgdCollateralProjection");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeEclPdCreditIndex_ObeEcls_ObeEclId",
                table: "ObeEclPdCreditIndex");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeLgdCollateralTypeDatas_ObeEcls_ObeEclId",
                table: "ObeLgdCollateralTypeDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ObeLgdContractDatas_ObeEcls_ObeEclId",
                table: "ObeLgdContractDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_ObePdLifetimeBests_ObeEcls_ObeEclId",
                table: "ObePdLifetimeBests");

            migrationBuilder.DropForeignKey(
                name: "FK_ObePdLifetimeDownturns_ObeEcls_ObeEclId",
                table: "ObePdLifetimeDownturns");

            migrationBuilder.DropForeignKey(
                name: "FK_ObePdLifetimeOptimistics_ObeEcls_ObeEclId",
                table: "ObePdLifetimeOptimistics");

            migrationBuilder.DropForeignKey(
                name: "FK_ObePdMappings_ObeEcls_ObeEclId",
                table: "ObePdMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_ObePdRedefaultLifetimeBests_ObeEcls_ObeEclId",
                table: "ObePdRedefaultLifetimeBests");

            migrationBuilder.DropForeignKey(
                name: "FK_ObePdRedefaultLifetimeDownturns_ObeEcls_ObeEclId",
                table: "ObePdRedefaultLifetimeDownturns");

            migrationBuilder.DropForeignKey(
                name: "FK_ObePdRedefaultLifetimeOptimistics_ObeEcls_ObeEclId",
                table: "ObePdRedefaultLifetimeOptimistics");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailEadCirProjections_RetailEcls_RetailEclId",
                table: "RetailEadCirProjections");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailEadEirProjetions_RetailEcls_RetailEclId",
                table: "RetailEadEirProjetions");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailEadInputs_RetailEcls_RetailEclId",
                table: "RetailEadInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailEadLifetimeProjections_RetailEcls_RetailEclId",
                table: "RetailEadLifetimeProjections");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailECLFrameworkFinal_RetailEcls_RetailEclId",
                table: "RetailECLFrameworkFinal");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailECLFrameworkFinalOverride_RetailEcls_RetailEclId",
                table: "RetailECLFrameworkFinalOverride");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailLGDCollateral_RetailEcls_RetailEclId",
                table: "RetailLGDCollateral");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailLgdCollateralProjection_RetailEcls_RetailEclId",
                table: "RetailLgdCollateralProjection");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailLgdCollateralTypeDatas_RetailEcls_RetailEclId",
                table: "RetailLgdCollateralTypeDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailLgdContractDatas_RetailEcls_RetailEclId",
                table: "RetailLgdContractDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailPDCreditIndex_RetailEcls_RetailEclId",
                table: "RetailPDCreditIndex");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailPdLifetimeBests_RetailEcls_RetailEclId",
                table: "RetailPdLifetimeBests");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailPdLifetimeDownturns_RetailEcls_RetailEclId",
                table: "RetailPdLifetimeDownturns");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailPdLifetimeOptimistics_RetailEcls_RetailEclId",
                table: "RetailPdLifetimeOptimistics");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailPdMappings_RetailEcls_RetailEclId",
                table: "RetailPdMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailPdRedefaultLifetimeBests_RetailEcls_RetailEclId",
                table: "RetailPdRedefaultLifetimeBests");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailPdRedefaultLifetimeDownturns_RetailEcls_RetailEclId",
                table: "RetailPdRedefaultLifetimeDownturns");

            migrationBuilder.DropForeignKey(
                name: "FK_RetailPdRedefaultLifetimeOptimistics_RetailEcls_RetailEclId",
                table: "RetailPdRedefaultLifetimeOptimistics");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEadCirProjections_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEadCirProjections");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEadEirProjections_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEadEirProjections");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEadInputs_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEadInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEadLifetimeProjections_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEadLifetimeProjections");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleECLFrameworkFinal_WholesaleEcls_WholesaleEclId",
                table: "WholesaleECLFrameworkFinal");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleECLFrameworkFinalOverride_WholesaleEcls_WholesaleEclId",
                table: "WholesaleECLFrameworkFinalOverride");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclSicrApprovals_WholesaleEclSicrs_WholesaleEclSicrId",
                table: "WholesaleEclSicrApprovals");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleEclSicrs_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrs");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleLGDCollateral_WholesaleEcls_WholesaleEclId",
                table: "WholesaleLGDCollateral");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleLgdCollateralProjection_WholesaleEcls_WholesaleEclId",
                table: "WholesaleLgdCollateralProjection");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleLgdCollateralTypeDatas_WholesaleEcls_WholesaleEclId",
                table: "WholesaleLgdCollateralTypeDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesaleLgdContractDatas_WholesaleEcls_WholesaleEclId",
                table: "WholesaleLgdContractDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalePDCreditIndex_WholesaleEcls_WholesaleEclId",
                table: "WholesalePDCreditIndex");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalePdLifetimeBests_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdLifetimeBests");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalePdLifetimeDownturns_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdLifetimeDownturns");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalePdLifetimeOptimistics_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdLifetimeOptimistics");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalePdMappings_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalePdRedefaultLifetimeBests_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeBests");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalePdRedefaultLifetimeDownturns_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeDownturns");

            migrationBuilder.DropForeignKey(
                name: "FK_WholesalePdRedefaultLifetimeOptimistics_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeOptimistics");

            migrationBuilder.DropIndex(
                name: "IX_WholesalePdRedefaultLifetimeOptimistics_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeOptimistics");

            migrationBuilder.DropIndex(
                name: "IX_WholesalePdRedefaultLifetimeDownturns_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeDownturns");

            migrationBuilder.DropIndex(
                name: "IX_WholesalePdRedefaultLifetimeBests_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeBests");

            migrationBuilder.DropIndex(
                name: "IX_WholesalePdMappings_WholesaleEclId",
                table: "WholesalePdMappings");

            migrationBuilder.DropIndex(
                name: "IX_WholesalePdLifetimeOptimistics_WholesaleEclId",
                table: "WholesalePdLifetimeOptimistics");

            migrationBuilder.DropIndex(
                name: "IX_WholesalePdLifetimeDownturns_WholesaleEclId",
                table: "WholesalePdLifetimeDownturns");

            migrationBuilder.DropIndex(
                name: "IX_WholesalePdLifetimeBests_WholesaleEclId",
                table: "WholesalePdLifetimeBests");

            migrationBuilder.DropIndex(
                name: "IX_WholesalePDCreditIndex_WholesaleEclId",
                table: "WholesalePDCreditIndex");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleLgdCollateralTypeDatas_WholesaleEclId",
                table: "WholesaleLgdCollateralTypeDatas");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleLgdCollateralProjection_WholesaleEclId",
                table: "WholesaleLgdCollateralProjection");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleLGDCollateral_WholesaleEclId",
                table: "WholesaleLGDCollateral");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclSicrs_WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrs");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEclSicrApprovals_WholesaleEclSicrId",
                table: "WholesaleEclSicrApprovals");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleECLFrameworkFinalOverride_WholesaleEclId",
                table: "WholesaleECLFrameworkFinalOverride");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleECLFrameworkFinal_WholesaleEclId",
                table: "WholesaleECLFrameworkFinal");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEadLifetimeProjections_WholesaleEclId",
                table: "WholesaleEadLifetimeProjections");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEadInputs_WholesaleEclId",
                table: "WholesaleEadInputs");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEadEirProjections_WholesaleEclId",
                table: "WholesaleEadEirProjections");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleEadCirProjections_WholesaleEclId",
                table: "WholesaleEadCirProjections");

            migrationBuilder.DropIndex(
                name: "IX_RetailPdRedefaultLifetimeOptimistics_RetailEclId",
                table: "RetailPdRedefaultLifetimeOptimistics");

            migrationBuilder.DropIndex(
                name: "IX_RetailPdRedefaultLifetimeDownturns_RetailEclId",
                table: "RetailPdRedefaultLifetimeDownturns");

            migrationBuilder.DropIndex(
                name: "IX_RetailPdRedefaultLifetimeBests_RetailEclId",
                table: "RetailPdRedefaultLifetimeBests");

            migrationBuilder.DropIndex(
                name: "IX_RetailPdMappings_RetailEclId",
                table: "RetailPdMappings");

            migrationBuilder.DropIndex(
                name: "IX_RetailPdLifetimeOptimistics_RetailEclId",
                table: "RetailPdLifetimeOptimistics");

            migrationBuilder.DropIndex(
                name: "IX_RetailPdLifetimeDownturns_RetailEclId",
                table: "RetailPdLifetimeDownturns");

            migrationBuilder.DropIndex(
                name: "IX_RetailPdLifetimeBests_RetailEclId",
                table: "RetailPdLifetimeBests");

            migrationBuilder.DropIndex(
                name: "IX_RetailPDCreditIndex_RetailEclId",
                table: "RetailPDCreditIndex");

            migrationBuilder.DropIndex(
                name: "IX_RetailLgdCollateralTypeDatas_RetailEclId",
                table: "RetailLgdCollateralTypeDatas");

            migrationBuilder.DropIndex(
                name: "IX_RetailLgdCollateralProjection_RetailEclId",
                table: "RetailLgdCollateralProjection");

            migrationBuilder.DropIndex(
                name: "IX_RetailLGDCollateral_RetailEclId",
                table: "RetailLGDCollateral");

            migrationBuilder.DropIndex(
                name: "IX_RetailECLFrameworkFinalOverride_RetailEclId",
                table: "RetailECLFrameworkFinalOverride");

            migrationBuilder.DropIndex(
                name: "IX_RetailECLFrameworkFinal_RetailEclId",
                table: "RetailECLFrameworkFinal");

            migrationBuilder.DropIndex(
                name: "IX_RetailEadLifetimeProjections_RetailEclId",
                table: "RetailEadLifetimeProjections");

            migrationBuilder.DropIndex(
                name: "IX_RetailEadInputs_RetailEclId",
                table: "RetailEadInputs");

            migrationBuilder.DropIndex(
                name: "IX_RetailEadCirProjections_RetailEclId",
                table: "RetailEadCirProjections");

            migrationBuilder.DropIndex(
                name: "IX_ObePdRedefaultLifetimeOptimistics_ObeEclId",
                table: "ObePdRedefaultLifetimeOptimistics");

            migrationBuilder.DropIndex(
                name: "IX_ObePdRedefaultLifetimeDownturns_ObeEclId",
                table: "ObePdRedefaultLifetimeDownturns");

            migrationBuilder.DropIndex(
                name: "IX_ObePdRedefaultLifetimeBests_ObeEclId",
                table: "ObePdRedefaultLifetimeBests");

            migrationBuilder.DropIndex(
                name: "IX_ObePdMappings_ObeEclId",
                table: "ObePdMappings");

            migrationBuilder.DropIndex(
                name: "IX_ObePdLifetimeOptimistics_ObeEclId",
                table: "ObePdLifetimeOptimistics");

            migrationBuilder.DropIndex(
                name: "IX_ObePdLifetimeDownturns_ObeEclId",
                table: "ObePdLifetimeDownturns");

            migrationBuilder.DropIndex(
                name: "IX_ObePdLifetimeBests_ObeEclId",
                table: "ObePdLifetimeBests");

            migrationBuilder.DropIndex(
                name: "IX_ObeLgdCollateralTypeDatas_ObeEclId",
                table: "ObeLgdCollateralTypeDatas");

            migrationBuilder.DropIndex(
                name: "IX_ObeEadInputs_ObeEclId",
                table: "ObeEadInputs");

            migrationBuilder.DropIndex(
                name: "IX_ObeEadEirProjections_ObeEclId",
                table: "ObeEadEirProjections");

            migrationBuilder.DropIndex(
                name: "IX_ObeEadCirProjections_ObeEclId",
                table: "ObeEadCirProjections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WholesaleLgdContractDatas",
                table: "WholesaleLgdContractDatas");

            migrationBuilder.DropIndex(
                name: "IX_WholesaleLgdContractDatas_WholesaleEclId",
                table: "WholesaleLgdContractDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RetailLgdContractDatas",
                table: "RetailLgdContractDatas");

            migrationBuilder.DropIndex(
                name: "IX_RetailLgdContractDatas_RetailEclId",
                table: "RetailLgdContractDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RetailEadEirProjetions",
                table: "RetailEadEirProjetions");

            migrationBuilder.DropIndex(
                name: "IX_RetailEadEirProjetions_RetailEclId",
                table: "RetailEadEirProjetions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeLgdContractDatas",
                table: "ObeLgdContractDatas");

            migrationBuilder.DropIndex(
                name: "IX_ObeLgdContractDatas_ObeEclId",
                table: "ObeLgdContractDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeEclPdCreditIndex",
                table: "ObeEclPdCreditIndex");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclPdCreditIndex_ObeEclId",
                table: "ObeEclPdCreditIndex");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeEclLgdCollateralProjection",
                table: "ObeEclLgdCollateralProjection");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclLgdCollateralProjection_ObeEclId",
                table: "ObeEclLgdCollateralProjection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeEclLgdCollateral",
                table: "ObeEclLgdCollateral");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclLgdCollateral_ObeEclId",
                table: "ObeEclLgdCollateral");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeEclFrameworkFinals",
                table: "ObeEclFrameworkFinals");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclFrameworkFinals_ObeEclId",
                table: "ObeEclFrameworkFinals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeEclFrameworkFinalOverrides",
                table: "ObeEclFrameworkFinalOverrides");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclFrameworkFinalOverrides_ObeEclId",
                table: "ObeEclFrameworkFinalOverrides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeEclEadLifetimeProjections",
                table: "ObeEclEadLifetimeProjections");

            migrationBuilder.DropIndex(
                name: "IX_ObeEclEadLifetimeProjections_ObeEclId",
                table: "ObeEclEadLifetimeProjections");

            migrationBuilder.RenameTable(
                name: "WholesaleLgdContractDatas",
                newName: "WholesaleLGDAccountData");

            migrationBuilder.RenameTable(
                name: "RetailLgdContractDatas",
                newName: "RetailLGDAccountData");

            migrationBuilder.RenameTable(
                name: "RetailEadEirProjetions",
                newName: "RetailEadEirProjections");

            migrationBuilder.RenameTable(
                name: "ObeLgdContractDatas",
                newName: "ObeLGDAccountData");

            migrationBuilder.RenameTable(
                name: "ObeEclPdCreditIndex",
                newName: "ObePDCreditIndex");

            migrationBuilder.RenameTable(
                name: "ObeEclLgdCollateralProjection",
                newName: "ObeLgdCollateralProjection");

            migrationBuilder.RenameTable(
                name: "ObeEclLgdCollateral",
                newName: "ObeLGDCollateral");

            migrationBuilder.RenameTable(
                name: "ObeEclFrameworkFinals",
                newName: "ObeECLFrameworkFinal");

            migrationBuilder.RenameTable(
                name: "ObeEclFrameworkFinalOverrides",
                newName: "ObeECLFrameworkFinalOverride");

            migrationBuilder.RenameTable(
                name: "ObeEclEadLifetimeProjections",
                newName: "ObeEadLifetimeProjections");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WholesaleLGDAccountData",
                table: "WholesaleLGDAccountData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RetailLGDAccountData",
                table: "RetailLGDAccountData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RetailEadEirProjections",
                table: "RetailEadEirProjections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeLGDAccountData",
                table: "ObeLGDAccountData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObePDCreditIndex",
                table: "ObePDCreditIndex",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeLgdCollateralProjection",
                table: "ObeLgdCollateralProjection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeLGDCollateral",
                table: "ObeLGDCollateral",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeECLFrameworkFinal",
                table: "ObeECLFrameworkFinal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeECLFrameworkFinalOverride",
                table: "ObeECLFrameworkFinalOverride",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeEadLifetimeProjections",
                table: "ObeEadLifetimeProjections",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WholesaleLGDAccountData",
                table: "WholesaleLGDAccountData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RetailLGDAccountData",
                table: "RetailLGDAccountData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RetailEadEirProjections",
                table: "RetailEadEirProjections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObePDCreditIndex",
                table: "ObePDCreditIndex");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeLgdCollateralProjection",
                table: "ObeLgdCollateralProjection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeLGDCollateral",
                table: "ObeLGDCollateral");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeLGDAccountData",
                table: "ObeLGDAccountData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeECLFrameworkFinalOverride",
                table: "ObeECLFrameworkFinalOverride");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeECLFrameworkFinal",
                table: "ObeECLFrameworkFinal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObeEadLifetimeProjections",
                table: "ObeEadLifetimeProjections");

            migrationBuilder.RenameTable(
                name: "WholesaleLGDAccountData",
                newName: "WholesaleLgdContractDatas");

            migrationBuilder.RenameTable(
                name: "RetailLGDAccountData",
                newName: "RetailLgdContractDatas");

            migrationBuilder.RenameTable(
                name: "RetailEadEirProjections",
                newName: "RetailEadEirProjetions");

            migrationBuilder.RenameTable(
                name: "ObePDCreditIndex",
                newName: "ObeEclPdCreditIndex");

            migrationBuilder.RenameTable(
                name: "ObeLgdCollateralProjection",
                newName: "ObeEclLgdCollateralProjection");

            migrationBuilder.RenameTable(
                name: "ObeLGDCollateral",
                newName: "ObeEclLgdCollateral");

            migrationBuilder.RenameTable(
                name: "ObeLGDAccountData",
                newName: "ObeLgdContractDatas");

            migrationBuilder.RenameTable(
                name: "ObeECLFrameworkFinalOverride",
                newName: "ObeEclFrameworkFinalOverrides");

            migrationBuilder.RenameTable(
                name: "ObeECLFrameworkFinal",
                newName: "ObeEclFrameworkFinals");

            migrationBuilder.RenameTable(
                name: "ObeEadLifetimeProjections",
                newName: "ObeEclEadLifetimeProjections");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WholesaleLgdContractDatas",
                table: "WholesaleLgdContractDatas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RetailLgdContractDatas",
                table: "RetailLgdContractDatas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RetailEadEirProjetions",
                table: "RetailEadEirProjetions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeEclPdCreditIndex",
                table: "ObeEclPdCreditIndex",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeEclLgdCollateralProjection",
                table: "ObeEclLgdCollateralProjection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeEclLgdCollateral",
                table: "ObeEclLgdCollateral",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeLgdContractDatas",
                table: "ObeLgdContractDatas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeEclFrameworkFinalOverrides",
                table: "ObeEclFrameworkFinalOverrides",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeEclFrameworkFinals",
                table: "ObeEclFrameworkFinals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObeEclEadLifetimeProjections",
                table: "ObeEclEadLifetimeProjections",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdRedefaultLifetimeOptimistics_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeOptimistics",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdRedefaultLifetimeDownturns_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeDownturns",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdRedefaultLifetimeBests_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeBests",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdMappings_WholesaleEclId",
                table: "WholesalePdMappings",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdLifetimeOptimistics_WholesaleEclId",
                table: "WholesalePdLifetimeOptimistics",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdLifetimeDownturns_WholesaleEclId",
                table: "WholesalePdLifetimeDownturns",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePdLifetimeBests_WholesaleEclId",
                table: "WholesalePdLifetimeBests",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesalePDCreditIndex_WholesaleEclId",
                table: "WholesalePDCreditIndex",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleLgdCollateralTypeDatas_WholesaleEclId",
                table: "WholesaleLgdCollateralTypeDatas",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleLgdCollateralProjection_WholesaleEclId",
                table: "WholesaleLgdCollateralProjection",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleLGDCollateral_WholesaleEclId",
                table: "WholesaleLGDCollateral",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclSicrs_WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrs",
                column: "WholesaleEclDataLoanBookId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEclSicrApprovals_WholesaleEclSicrId",
                table: "WholesaleEclSicrApprovals",
                column: "WholesaleEclSicrId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleECLFrameworkFinalOverride_WholesaleEclId",
                table: "WholesaleECLFrameworkFinalOverride",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleECLFrameworkFinal_WholesaleEclId",
                table: "WholesaleECLFrameworkFinal",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEadLifetimeProjections_WholesaleEclId",
                table: "WholesaleEadLifetimeProjections",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEadInputs_WholesaleEclId",
                table: "WholesaleEadInputs",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEadEirProjections_WholesaleEclId",
                table: "WholesaleEadEirProjections",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleEadCirProjections_WholesaleEclId",
                table: "WholesaleEadCirProjections",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdRedefaultLifetimeOptimistics_RetailEclId",
                table: "RetailPdRedefaultLifetimeOptimistics",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdRedefaultLifetimeDownturns_RetailEclId",
                table: "RetailPdRedefaultLifetimeDownturns",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdRedefaultLifetimeBests_RetailEclId",
                table: "RetailPdRedefaultLifetimeBests",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdMappings_RetailEclId",
                table: "RetailPdMappings",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdLifetimeOptimistics_RetailEclId",
                table: "RetailPdLifetimeOptimistics",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdLifetimeDownturns_RetailEclId",
                table: "RetailPdLifetimeDownturns",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPdLifetimeBests_RetailEclId",
                table: "RetailPdLifetimeBests",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailPDCreditIndex_RetailEclId",
                table: "RetailPDCreditIndex",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailLgdCollateralTypeDatas_RetailEclId",
                table: "RetailLgdCollateralTypeDatas",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailLgdCollateralProjection_RetailEclId",
                table: "RetailLgdCollateralProjection",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailLGDCollateral_RetailEclId",
                table: "RetailLGDCollateral",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailECLFrameworkFinalOverride_RetailEclId",
                table: "RetailECLFrameworkFinalOverride",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailECLFrameworkFinal_RetailEclId",
                table: "RetailECLFrameworkFinal",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEadLifetimeProjections_RetailEclId",
                table: "RetailEadLifetimeProjections",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEadInputs_RetailEclId",
                table: "RetailEadInputs",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEadCirProjections_RetailEclId",
                table: "RetailEadCirProjections",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdRedefaultLifetimeOptimistics_ObeEclId",
                table: "ObePdRedefaultLifetimeOptimistics",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdRedefaultLifetimeDownturns_ObeEclId",
                table: "ObePdRedefaultLifetimeDownturns",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdRedefaultLifetimeBests_ObeEclId",
                table: "ObePdRedefaultLifetimeBests",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdMappings_ObeEclId",
                table: "ObePdMappings",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdLifetimeOptimistics_ObeEclId",
                table: "ObePdLifetimeOptimistics",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdLifetimeDownturns_ObeEclId",
                table: "ObePdLifetimeDownturns",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObePdLifetimeBests_ObeEclId",
                table: "ObePdLifetimeBests",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeLgdCollateralTypeDatas_ObeEclId",
                table: "ObeLgdCollateralTypeDatas",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEadInputs_ObeEclId",
                table: "ObeEadInputs",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEadEirProjections_ObeEclId",
                table: "ObeEadEirProjections",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEadCirProjections_ObeEclId",
                table: "ObeEadCirProjections",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_WholesaleLgdContractDatas_WholesaleEclId",
                table: "WholesaleLgdContractDatas",
                column: "WholesaleEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailLgdContractDatas_RetailEclId",
                table: "RetailLgdContractDatas",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_RetailEadEirProjetions_RetailEclId",
                table: "RetailEadEirProjetions",
                column: "RetailEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclPdCreditIndex_ObeEclId",
                table: "ObeEclPdCreditIndex",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclLgdCollateralProjection_ObeEclId",
                table: "ObeEclLgdCollateralProjection",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclLgdCollateral_ObeEclId",
                table: "ObeEclLgdCollateral",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeLgdContractDatas_ObeEclId",
                table: "ObeLgdContractDatas",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclFrameworkFinalOverrides_ObeEclId",
                table: "ObeEclFrameworkFinalOverrides",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclFrameworkFinals_ObeEclId",
                table: "ObeEclFrameworkFinals",
                column: "ObeEclId");

            migrationBuilder.CreateIndex(
                name: "IX_ObeEclEadLifetimeProjections_ObeEclId",
                table: "ObeEclEadLifetimeProjections",
                column: "ObeEclId");

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEadCirProjections_ObeEcls_ObeEclId",
                table: "ObeEadCirProjections",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEadEirProjections_ObeEcls_ObeEclId",
                table: "ObeEadEirProjections",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEadInputs_ObeEcls_ObeEclId",
                table: "ObeEadInputs",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclEadLifetimeProjections_ObeEcls_ObeEclId",
                table: "ObeEclEadLifetimeProjections",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclFrameworkFinalOverrides_ObeEcls_ObeEclId",
                table: "ObeEclFrameworkFinalOverrides",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclFrameworkFinals_ObeEcls_ObeEclId",
                table: "ObeEclFrameworkFinals",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclLgdCollateral_ObeEcls_ObeEclId",
                table: "ObeEclLgdCollateral",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclLgdCollateralProjection_ObeEcls_ObeEclId",
                table: "ObeEclLgdCollateralProjection",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeEclPdCreditIndex_ObeEcls_ObeEclId",
                table: "ObeEclPdCreditIndex",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeLgdCollateralTypeDatas_ObeEcls_ObeEclId",
                table: "ObeLgdCollateralTypeDatas",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObeLgdContractDatas_ObeEcls_ObeEclId",
                table: "ObeLgdContractDatas",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObePdLifetimeBests_ObeEcls_ObeEclId",
                table: "ObePdLifetimeBests",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObePdLifetimeDownturns_ObeEcls_ObeEclId",
                table: "ObePdLifetimeDownturns",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObePdLifetimeOptimistics_ObeEcls_ObeEclId",
                table: "ObePdLifetimeOptimistics",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObePdMappings_ObeEcls_ObeEclId",
                table: "ObePdMappings",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObePdRedefaultLifetimeBests_ObeEcls_ObeEclId",
                table: "ObePdRedefaultLifetimeBests",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ObePdRedefaultLifetimeDownturns_ObeEcls_ObeEclId",
                table: "ObePdRedefaultLifetimeDownturns",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ObePdRedefaultLifetimeOptimistics_ObeEcls_ObeEclId",
                table: "ObePdRedefaultLifetimeOptimistics",
                column: "ObeEclId",
                principalTable: "ObeEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailEadCirProjections_RetailEcls_RetailEclId",
                table: "RetailEadCirProjections",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailEadEirProjetions_RetailEcls_RetailEclId",
                table: "RetailEadEirProjetions",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailEadInputs_RetailEcls_RetailEclId",
                table: "RetailEadInputs",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailEadLifetimeProjections_RetailEcls_RetailEclId",
                table: "RetailEadLifetimeProjections",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailECLFrameworkFinal_RetailEcls_RetailEclId",
                table: "RetailECLFrameworkFinal",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailECLFrameworkFinalOverride_RetailEcls_RetailEclId",
                table: "RetailECLFrameworkFinalOverride",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailLGDCollateral_RetailEcls_RetailEclId",
                table: "RetailLGDCollateral",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailLgdCollateralProjection_RetailEcls_RetailEclId",
                table: "RetailLgdCollateralProjection",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailLgdCollateralTypeDatas_RetailEcls_RetailEclId",
                table: "RetailLgdCollateralTypeDatas",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailLgdContractDatas_RetailEcls_RetailEclId",
                table: "RetailLgdContractDatas",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailPDCreditIndex_RetailEcls_RetailEclId",
                table: "RetailPDCreditIndex",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailPdLifetimeBests_RetailEcls_RetailEclId",
                table: "RetailPdLifetimeBests",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailPdLifetimeDownturns_RetailEcls_RetailEclId",
                table: "RetailPdLifetimeDownturns",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailPdLifetimeOptimistics_RetailEcls_RetailEclId",
                table: "RetailPdLifetimeOptimistics",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailPdMappings_RetailEcls_RetailEclId",
                table: "RetailPdMappings",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailPdRedefaultLifetimeBests_RetailEcls_RetailEclId",
                table: "RetailPdRedefaultLifetimeBests",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailPdRedefaultLifetimeDownturns_RetailEcls_RetailEclId",
                table: "RetailPdRedefaultLifetimeDownturns",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RetailPdRedefaultLifetimeOptimistics_RetailEcls_RetailEclId",
                table: "RetailPdRedefaultLifetimeOptimistics",
                column: "RetailEclId",
                principalTable: "RetailEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEadCirProjections_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEadCirProjections",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEadEirProjections_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEadEirProjections",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEadInputs_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEadInputs",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEadLifetimeProjections_WholesaleEcls_WholesaleEclId",
                table: "WholesaleEadLifetimeProjections",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleECLFrameworkFinal_WholesaleEcls_WholesaleEclId",
                table: "WholesaleECLFrameworkFinal",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleECLFrameworkFinalOverride_WholesaleEcls_WholesaleEclId",
                table: "WholesaleECLFrameworkFinalOverride",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclSicrApprovals_WholesaleEclSicrs_WholesaleEclSicrId",
                table: "WholesaleEclSicrApprovals",
                column: "WholesaleEclSicrId",
                principalTable: "WholesaleEclSicrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleEclSicrs_WholesaleEclDataLoanBooks_WholesaleEclDataLoanBookId",
                table: "WholesaleEclSicrs",
                column: "WholesaleEclDataLoanBookId",
                principalTable: "WholesaleEclDataLoanBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleLGDCollateral_WholesaleEcls_WholesaleEclId",
                table: "WholesaleLGDCollateral",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleLgdCollateralProjection_WholesaleEcls_WholesaleEclId",
                table: "WholesaleLgdCollateralProjection",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleLgdCollateralTypeDatas_WholesaleEcls_WholesaleEclId",
                table: "WholesaleLgdCollateralTypeDatas",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesaleLgdContractDatas_WholesaleEcls_WholesaleEclId",
                table: "WholesaleLgdContractDatas",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalePDCreditIndex_WholesaleEcls_WholesaleEclId",
                table: "WholesalePDCreditIndex",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalePdLifetimeBests_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdLifetimeBests",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalePdLifetimeDownturns_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdLifetimeDownturns",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalePdLifetimeOptimistics_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdLifetimeOptimistics",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalePdMappings_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdMappings",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalePdRedefaultLifetimeBests_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeBests",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalePdRedefaultLifetimeDownturns_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeDownturns",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WholesalePdRedefaultLifetimeOptimistics_WholesaleEcls_WholesaleEclId",
                table: "WholesalePdRedefaultLifetimeOptimistics",
                column: "WholesaleEclId",
                principalTable: "WholesaleEcls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
