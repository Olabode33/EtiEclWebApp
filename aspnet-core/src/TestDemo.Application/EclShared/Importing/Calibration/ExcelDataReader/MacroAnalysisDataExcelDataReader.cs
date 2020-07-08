using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Localization;
using Abp.Localization.Sources;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TestDemo.AffiliateMacroEconomicVariable;
using TestDemo.DataExporting.Excel.EpPlus;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.EclShared.Importing.Dto;
using TestDemo.EclShared.Importing.Utils;

namespace TestDemo.EclShared.Importing
{
    public class MacroAnalysisDataExcelDataReader : EpPlusExcelImporterBase<ImportMacroAnalysisDataDto>, IMacroAnalysisDataExcelDataReader
    {
        private readonly ILocalizationSource _localizationSource;
        private List<NameValueDto<int>> _affiliateMacroVariable;
        private readonly IValidationUtil _validator;

        public MacroAnalysisDataExcelDataReader(ILocalizationManager localizationManager,
            IValidationUtil validator)
        {
            _localizationSource = localizationManager.GetSource(TestDemoConsts.LocalizationSourceName);
            _affiliateMacroVariable = new List<NameValueDto<int>>();
            _validator = validator;
        }

        public List<ImportMacroAnalysisDataDto> GetImportMacroAnalysisDataFromExcel(byte[] fileBytes, List<NameValueDto<int>> affiliateMacroVariables)
        {
            _affiliateMacroVariable = affiliateMacroVariables;
            return ProcessExcelFile(fileBytes, ProcessExcelRow);
        }

        public List<ImportMacroAnalysisDataDto> ProcessExcelFile(byte[] fileBytes, Func<ExcelWorksheet, int, List<ImportMacroAnalysisDataDto>> processExcelRow)
        {
            var entities = new List<ImportMacroAnalysisDataDto>();

            using (var stream = new MemoryStream(fileBytes))
            {
                using (var excelPackage = new ExcelPackage(stream))
                {
                    foreach (var worksheet in excelPackage.Workbook.Worksheets)
                    {
                        var entitiesInWorksheet = ProcessWorksheet(worksheet, processExcelRow);

                        entities.AddRange(entitiesInWorksheet);
                    }
                }
            }

            return entities;
        }

        private List<ImportMacroAnalysisDataDto> ProcessWorksheet(ExcelWorksheet worksheet, Func<ExcelWorksheet, int, List<ImportMacroAnalysisDataDto>> processExcelRow)
        {
            var entities = new List<ImportMacroAnalysisDataDto>();

            for (var i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
            {
                try
                {
                    var entity = processExcelRow(worksheet, i);

                    if (entity != null)
                    {
                        entities.AddRange(entity);
                    }
                }
                catch (Exception)
                {
                    //ignore
                }
            }

            return entities;
        }

        private List<ImportMacroAnalysisDataDto> ProcessExcelRow(ExcelWorksheet worksheet, int row)
        {
            if (IsRowEmpty(worksheet, row))
            {
                return null;
            }

            var exceptionMessage = new StringBuilder();
            var list = new List<ImportMacroAnalysisDataDto>();

            if (_affiliateMacroVariable.Count > 0)
            {
                ///Read NPL Value
                var nplData = new ImportMacroAnalysisDataDto();
                try
                {
                    nplData.Period = _validator.GetDateTimeValueFromRowOrNull(worksheet, row, 1, nameof(nplData.Period), exceptionMessage);
                    nplData.MacroeconomicId = -1;
                    nplData.Value = _validator.GetDoubleValueFromRowOrNull(worksheet, row, 2, "NPL_Percentage_Ratio", exceptionMessage);

                }
                catch (Exception exception)
                {
                    nplData.Exception = exception.Message;
                }
                if (exceptionMessage.Length > 0)
                {
                    nplData.Exception = exceptionMessage.ToString();
                }
                list.Add(nplData);

                //Read Macroeconomic values
                for (int i = 0; i < _affiliateMacroVariable.Count; i++)
                {
                    var data = new ImportMacroAnalysisDataDto();
                    var exceptionMsg = new StringBuilder();
                    try
                    {
                        data.Period = _validator.GetDateTimeValueFromRowOrNull(worksheet, row, 1, nameof(data.Period), exceptionMsg);
                        data.MacroeconomicId = _affiliateMacroVariable[i].Value;
                        data.Value = _validator.GetDoubleValueFromRowOrNull(worksheet, row, i + 3, _affiliateMacroVariable[i].Name, exceptionMsg);

                    }
                    catch (Exception exception)
                    {
                        data.Exception = exception.Message;
                    }
                    if (exceptionMsg.Length > 0)
                    {
                        data.Exception = exceptionMsg.ToString();
                    }
                    list.Add(data);
                }
            }
            else
            {
                return null;
            }

            return list;
        }

        

        private string GetLocalizedExceptionMessagePart(string parameter, string required)
        {
            return _localizationSource.GetString("{0}IsInvalid", _localizationSource.GetString(parameter)) + " " + _localizationSource.GetString(required) + "; ";
        }


        private bool IsRowEmpty(ExcelWorksheet worksheet, int row)
        {
            return worksheet.Cells[row, 1].Value == null || string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Value.ToString());
        }
    }
}
