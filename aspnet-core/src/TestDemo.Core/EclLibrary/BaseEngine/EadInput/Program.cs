using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAD_Inputs_Automation
{
    class Program
    {
        protected const string uploadID = "4FE329C8-C57F-4EB2-8F7F-08D75BC1F14A";
        static void Main(string[] args)
        {
            ///////
            ///read the raw data 
            ///////
            ///////
            ///
            //string rawDatasample = @"C:\Users\tarokodare001\Documents\WORK\ECOBANK\Model Excels\Nigeria\Raw Data\Raw data - Copy.xlsx";
            //@"C: \Users\tarokodare001\Documents\WORK\ECOBANK\My Sample Excel\EAD\Ead Inputs.xlsx";

            DataTable rawData = Helper.ReadData(@"SELECT [Id]
                                                    ,[CreationTime]
                                                    ,[CreatorUserId]
                                                    ,[LastModificationTime]
                                                    ,[LastModifierUserId]
                                                    ,[IsDeleted]
                                                    ,[DeleterUserId]
                                                    ,[DeletionTime]
                                                    ,[TenantId]
                                                    ,[CustomerNo]
                                                    ,[AccountNo]
                                                    ,[ContractNo]
                                                    ,[CustomerName]
                                                    ,[SnapshotDate]
                                                    ,[Segment]
                                                    ,[Sector]
                                                    ,[Currency]
                                                    ,[ProductType]
                                                    ,[ProductMapping]
                                                    ,[SpecialisedLending]
                                                    ,[RatingModel]
                                                    ,[OriginalRating]
                                                    ,[CurrentRating]
                                                    ,[LifetimePD]
                                                    ,[Month12PD]
                                                    ,[DaysPastDue]
                                                    ,[WatchlistIndicator]
                                                    ,[Classification]
                                                    ,[ImpairedDate]
                                                    ,[DefaultDate]
                                                    ,[CreditLimit]
                                                    ,[OriginalBalanceLCY]
                                                    ,[OutstandingBalanceLCY]
                                                    ,[OutstandingBalanceACY]
                                                    ,[ContractStartDate]
                                                    ,[ContractEndDate]
                                                    ,[RestructureIndicator]
                                                    ,[RestructureRisk]
                                                    ,[RestructureType]
                                                    ,[RestructureStartDate]
                                                    ,[RestructureEndDate]
                                                    ,[PrincipalPaymentTermsOrigination]
                                                    ,[PPTOPeriod]
                                                    ,[InterestPaymentTermsOrigination]
                                                    ,[IPTOPeriod]
                                                    ,[PrincipalPaymentStructure]
                                                    ,[InterestPaymentStructure]
                                                    ,[InterestRateType]
                                                    ,[BaseRate]
                                                    ,[OriginationContractualInterestRate]
                                                    ,[IntroductoryPeriod]
                                                    ,[PostIPContractualInterestRate]
                                                    ,[CurrentContractualInterestRate]
                                                    ,[EIR]
                                                    ,[DebentureOMV]
                                                    ,[DebentureFSV]
                                                    ,[CashOMV]
                                                    ,[CashFSV]
                                                    ,[InventoryOMV]
                                                    ,[InventoryFSV]
                                                    ,[PlantEquipmentOMV]
                                                    ,[PlantEquipmentFSV]
                                                    ,[ResidentialPropertyOMV]
                                                    ,[ResidentialPropertyFSV]
                                                    ,[CommercialPropertyOMV]
                                                    ,[CommercialProperty]
                                                    ,[ReceivablesOMV]
                                                    ,[ReceivablesFSV]
                                                    ,[SharesOMV]
                                                    ,[SharesFSV]
                                                    ,[VehicleOMV]
                                                    ,[VehicleFSV]
                                                    ,[CureRate]
                                                    ,[GuaranteeIndicator]
                                                    ,[GuarantorPD]
                                                    ,[GuarantorLGD]
                                                    ,[GuaranteeValue]
                                                    ,[GuaranteeLevel]
                                                    ,[ContractId]
                                                    ,[WholesaleEclUploadId]
                                                    ,[OrganizationUnitId]
                                                    FROM[ETI_IFRS9_DB].[dbo].[WholesaleEclDataLoanBooks]
                                                    WHERE WholesaleEclUploadId = (SELECT id from [ETI_IFRS9_DB].[dbo].[WholesaleEclUploads] where [WholesaleEclId] = '"+ uploadID +"')");

            EAD_Inputs_Calculation ead_input = new EAD_Inputs_Calculation();
            ead_input.EAD_Inputs_START(rawData);
            
        }
    }
}
