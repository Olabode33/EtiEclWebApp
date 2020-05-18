using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.AffiliateMacroEconomicVariable.Dtos;
using TestDemo.Dto;
using TestDemo.Storage;

namespace TestDemo.AffiliateMacroEconomicVariable.Exporting
{
    public class AffiliateMacroEconomicVariableOffsetsExcelExporter : EpPlusExcelExporterBase, IAffiliateMacroEconomicVariableOffsetsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public AffiliateMacroEconomicVariableOffsetsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
			ITempFileCacheManager tempFileCacheManager) :  
	base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetAffiliateMacroEconomicVariableOffsetForViewDto> affiliateMacroEconomicVariableOffsets)
        {
            return CreateExcelPackage(
                "AffiliateMacroEconomicVariableOffsets.xlsx",
                excelPackage =>
                {
                    var sheet = excelPackage.Workbook.Worksheets.Add(L("AffiliateMacroEconomicVariableOffsets"));
                    sheet.OutLineApplyStyle = true;

                    AddHeader(
                        sheet,
                        L("BackwardOffset"),
                        (L("OrganizationUnit")) + L("DisplayName"),
                        (L("MacroeconomicVariable")) + L("Name")
                        );

                    AddObjects(
                        sheet, 2, affiliateMacroEconomicVariableOffsets,
                        _ => _.AffiliateMacroEconomicVariableOffset.BackwardOffset,
                        _ => _.OrganizationUnitDisplayName,
                        _ => _.MacroeconomicVariableName
                        );

					

                });
        }
    }
}
