using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.LoanImpairmentScenarios.Dtos;
using TestDemo.Dto;
using TestDemo.Storage;

namespace TestDemo.LoanImpairmentScenarios.Exporting
{
    public class LoanImpairmentScenariosExcelExporter : EpPlusExcelExporterBase, ILoanImpairmentScenariosExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LoanImpairmentScenariosExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLoanImpairmentScenarioForViewDto> loanImpairmentScenarios)
        {
            return CreateExcelPackage(
                "LoanImpairmentScenarios.xlsx",
                excelPackage =>
                {
                    
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("LoanImpairmentScenarios"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet
                        );

                    AddObjects(
                        sheet, 2, loanImpairmentScenarios
                        );

					
					
                });
        }
    }
}
