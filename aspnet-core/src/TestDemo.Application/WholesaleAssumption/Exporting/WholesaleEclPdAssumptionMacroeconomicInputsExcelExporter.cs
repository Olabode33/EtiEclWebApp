using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.WholesaleAssumption.Dtos;
using TestDemo.Dto;
using TestDemo.Storage;

namespace TestDemo.WholesaleAssumption.Exporting
{
    public class WholesaleEclPdAssumptionMacroeconomicInputsExcelExporter : EpPlusExcelExporterBase, IWholesaleEclPdAssumptionMacroeconomicInputsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public WholesaleEclPdAssumptionMacroeconomicInputsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetWholesaleEclPdAssumptionMacroeconomicInputForViewDto> wholesaleEclPdAssumptionMacroeconomicInputs)
        {
            return CreateExcelPackage(
                "WholesaleEclPdAssumptionMacroeconomicInputs.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("WholesaleEclPdAssumptionMacroeconomicInputs"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        (L("WholesaleEcl")) + L("TenantId")
                        );

                    AddObjects(
                        sheet, 2, wholesaleEclPdAssumptionMacroeconomicInputs,
                        _ => _.WholesaleEclTenantId
                        );

					

                });
        }
    }
}
