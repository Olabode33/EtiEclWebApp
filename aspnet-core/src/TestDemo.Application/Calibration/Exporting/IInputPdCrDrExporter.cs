using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.HoldCoAssetBook.Dtos;
using TestDemo.HoldCoInterCompanyResults.Dtos;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.IVModels.Dtos;
using TestDemo.LoanImpairmentKeyParameters.Dtos;
using TestDemo.LoanImpairmentModelResults.Dtos;
using TestDemo.LoanImpairmentRecoveries.Dtos;
using TestDemo.LoanImpairmentScenarios.Dtos;
using TestDemo.ReceivablesCurrentPeriodDates.Dtos;
using TestDemo.ReceivablesForecasts.Dtos;
using TestDemo.ReceivablesResults.Dtos;

namespace TestDemo.Calibration.Exporting
{
    public interface IInputPdCrDrExporter
    {
        FileDto ExportToFile(List<InputPdCommsConsDto> inputDtos);
        FileDto ExportToFile(List<InputPdCrDrDto> inputDtos);

        FileDto ExportToFile(List<InputMacroEconomicCreditIndexDto> inputDtos);

        FileDto ExportToFile(List<InputAssetBookDto> inputDtos);
        
        FileDto ExportToFile(List<InputHoldCoInterCompanyResultDto> inputDtos);

        FileDto ExportToFile(List<InputHoldCoResultSummaryDto> inputDtos);

        FileDto ExportToFile(List<InputResultSummaryByStageDto> inputDtos);

        FileDto ExportToFile(List<InputCurrentPeriodDateDto> inputDtos);

        FileDto ExportToFile(List<InputReceivablesForecastDto> inputDtos);

        FileDto ExportToFile(List<InputReceivablesResultDto> inputDtos);
        FileDto ExportToFile(List<InputLoanImpairmentRecoveryDto> inputDtos);

        FileDto ExportToFile(List<InputLoanImpairmentKeyParameterDto> inputDtos);
        FileDto ExportToFile(List<InputLoanImpairmentScenarioDto> inputDtos);
        FileDto ExportToFile(List<InputLoanImpairmentModelResultDto> inputDtos);


    }
}