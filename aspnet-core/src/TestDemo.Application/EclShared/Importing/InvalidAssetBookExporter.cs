using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.Storage;

namespace TestDemo.EclShared.Importing
{
    public class InvalidAssetBookExporter : EpPlusExcelExporterBase, IInvalidAssetBookExporter
    {
        public InvalidAssetBookExporter(ITempFileCacheManager tempFileCacheManager)
             : base(tempFileCacheManager)
        {
        }

        public FileDto ExportToFile(List<ImportAssetBookDto> inputDtos)
        {
            return CreateExcelPackage(
                "InvalidAssetbookImportList.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("InvalidAssetbookImports"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("AssetDescription"),
                        L("AssetType"),
                        L("CounterParty"),
                        L("SovereignDebt"),
                        L("RatingAgency"),
                        L("CreditRatingAtPurchaseDate"),
                        L("CurrentCreditRating"),
                        L("NominalAmount"),
                        L("PrincipalAmortisation"),
                        L("RepaymentTerms"),
                        L("CarryAmountNGAAP"),
                        L("CarryingAmountIFRS"),
                        L("Coupon"),
                        L("Eir"),
                        L("PurchaseDate"),
                        L("IssueDate"),
                        L("PurchasePrice"),
                        L("MaturityDate"),
                        L("RedemptionPrice"),
                        L("BusinessModelClassification"),
                        L("Ias39Impairment"),
                        L("PrudentialClassification"),
                        L("ForebearanceFlag"),
                        L("RefuseReason")
                        );

                    AddObjects(
                        sheet, 2, inputDtos,
                        _ => _.AssetDescription,
                        _ => _.AssetType,
                        _ => _.CounterParty,
                        _ => _.SovereignDebt,
                        _ => _.RatingAgency,
                        _ => _.CreditRatingAtPurchaseDate,
                        _ => _.CurrentCreditRating,
                        _ => _.NominalAmount,
                        _ => _.PrincipalAmortisation,
                        _ => _.RepaymentTerms,
                        _ => _.CarryAmountNGAAP,
                        _ => _.CarryingAmountIFRS,
                        _ => _.Coupon,
                        _ => _.Eir,
                        _ => _.PurchaseDate,
                        _ => _.IssueDate,
                        _ => _.PurchasePrice,
                        _ => _.MaturityDate,
                        _ => _.RedemptionPrice,
                        _ => _.BusinessModelClassification,
                        _ => _.Ias39Impairment,
                        _ => _.PrudentialClassification,
                        _ => _.ForebearanceFlag,
                        _ => _.Exception
                    );

                    for (var i = 1; i <= 24; i++)
                    {
                        sheet.Column(i).AutoFit();
                    }
                });
        }
    }
}
