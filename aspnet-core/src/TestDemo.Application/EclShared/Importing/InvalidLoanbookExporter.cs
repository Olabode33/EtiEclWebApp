using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Storage;

namespace TestDemo.EclShared.Importing
{
    public class InvalidLoanbookExporter : EpPlusExcelExporterBase, IInvalidLoanbookExporter
    {
        public InvalidLoanbookExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportLoanbookDto> inputDtos)
        {
            return CreateExcelPackage(
                "InvalidLoanbookImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InvalidLoanbookImports"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("CustomerNo"),
                        L("AccountNo"),
                        L("ContractNo"),
                        L("CustomerName"),
                        L("SnapshotDate"),
                        L("Segment"),
                        L("Sector"),
                        L("Currency"),
                        L("ProductType"),
                        L("ProductMapping"),
                        L("SpecialisedLending"),
                        L("RatingModel"),
                        L("OriginalRating"),
                        L("CurrentRating"),
                        L("LifetimePD"),
                        L("Month12PD"),
                        L("DaysPastDue"),
                        L("WatchlistIndicator"),
                        L("Classification"),
                        L("ImpairedDate"),
                        L("DefaultDate"),
                        L("CreditLimit"),
                        L("OriginalBalanceLCY"),
                        L("OutstandingBalanceLCY"),
                        L("OutstandingBalanceACY"),
                        L("ContractStartDate"),
                        L("ContractEndDate"),
                        L("RestructureIndicator"),
                        L("RestructureRisk"),
                        L("RestructureType"),
                        L("RestructureStartDate"),
                        L("RestructureEndDate"),
                        L("PrincipalPaymentTermsOrigination"),
                        L("PPTOPeriod"),
                        L("InterestPaymentTermsOrigination"),
                        L("IPTOPeriod"),
                        L("PrincipalPaymentStructure"),
                        L("InterestPaymentStructure"),
                        L("InterestRateType"),
                        L("BaseRate"),
                        L("OriginationContractualInterestRate"),
                        L("IntroductoryPeriod"),
                        L("PostIPContractualInterestRate"),
                        L("CurrentContractualInterestRate"),
                        L("EIR"),
                        L("DebentureOMV"),
                        L("DebentureFSV"),
                        L("CashOMV"),
                        L("CashFSV"),
                        L("InventoryOMV"),
                        L("InventoryFSV"),
                        L("PlantEquipmentOMV"),
                        L("PlantEquipmentFSV"),
                        L("ResidentialPropertyOMV"),
                        L("ResidentialPropertyFSV"),
                        L("CommercialPropertyOMV"),
                        L("CommercialPropertyFSV"),
                        L("ReceivablesOMV"),
                        L("ReceivablesFSV"),
                        L("SharesOMV"),
                        L("SharesFSV"),
                        L("VehicleOMV"),
                        L("VehicleFSV"),
                        L("CureRate"),
                        L("GuaranteeIndicator"),
                        L("GuarantorPD"),
                        L("GuarantorLGD"),
                        L("GuaranteeValue"),
                        L("GuaranteeLevel"),
                        L("Exception"),
                        L("RefuseReason")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.CustomerNo,
                        _ => _.AccountNo,
                        _ => _.ContractNo,
                        _ => _.CustomerName,
                        _ => _.SnapshotDate,
                        _ => _.Segment,
                        _ => _.Sector,
                        _ => _.Currency,
                        _ => _.ProductType,
                        _ => _.ProductMapping,
                        _ => _.SpecialisedLending,
                        _ => _.RatingModel,
                        _ => _.OriginalRating,
                        _ => _.CurrentRating,
                        _ => _.LifetimePD,
                        _ => _.Month12PD,
                        _ => _.DaysPastDue,
                        _ => _.WatchlistIndicator,
                        _ => _.Classification,
                        _ => _.ImpairedDate,
                        _ => _.DefaultDate,
                        _ => _.CreditLimit,
                        _ => _.OriginalBalanceLCY,
                        _ => _.OutstandingBalanceLCY,
                        _ => _.OutstandingBalanceACY,
                        _ => _.ContractStartDate,
                        _ => _.ContractEndDate,
                        _ => _.RestructureIndicator,
                        _ => _.RestructureRisk,
                        _ => _.RestructureType,
                        _ => _.RestructureStartDate,
                        _ => _.RestructureEndDate,
                        _ => _.PrincipalPaymentTermsOrigination,
                        _ => _.PPTOPeriod,
                        _ => _.InterestPaymentTermsOrigination,
                        _ => _.IPTOPeriod,
                        _ => _.PrincipalPaymentStructure,
                        _ => _.InterestPaymentStructure,
                        _ => _.InterestRateType,
                        _ => _.BaseRate,
                        _ => _.OriginationContractualInterestRate,
                        _ => _.IntroductoryPeriod,
                        _ => _.PostIPContractualInterestRate,
                        _ => _.CurrentContractualInterestRate,
                        _ => _.EIR,
                        _ => _.DebentureOMV,
                        _ => _.DebentureFSV,
                        _ => _.CashOMV,
                        _ => _.CashFSV,
                        _ => _.InventoryOMV,
                        _ => _.InventoryFSV,
                        _ => _.PlantEquipmentOMV,
                        _ => _.PlantEquipmentFSV,
                        _ => _.ResidentialPropertyOMV,
                        _ => _.ResidentialPropertyFSV,
                        _ => _.CommercialPropertyOMV,
                        _ => _.CommercialPropertyFSV,
                        _ => _.ReceivablesOMV,
                        _ => _.ReceivablesFSV,
                        _ => _.SharesOMV,
                        _ => _.SharesFSV,
                        _ => _.VehicleOMV,
                        _ => _.VehicleFSV,
                        _ => _.CureRate,
                        _ => _.GuaranteeIndicator,
                        _ => _.GuarantorPD,
                        _ => _.GuarantorLGD,
                        _ => _.GuaranteeValue,
                        _ => _.GuaranteeLevel,
                        _ => _.Exception
                    );

                    for (var i = 1; i <= 69; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
