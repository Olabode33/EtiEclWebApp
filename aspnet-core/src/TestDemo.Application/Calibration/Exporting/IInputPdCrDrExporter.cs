using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.HoldCoAssetBook.Dtos;
using TestDemo.HoldCoInterCompanyResults.Dtos;
using TestDemo.HoldCoResult.Dtos;
using TestDemo.IVModels.Dtos;

namespace TestDemo.Calibration.Exporting
{
    public interface IInputPdCrDrExporter
    {
        FileDto ExportToFile(List<InputPdCrDrDto> inputDtos);

        FileDto ExportToFile(List<InputMacroEconomicCreditIndexDto> inputDtos);

        FileDto ExportToFile(List<InputAssetBookDto> inputDtos);
        
        FileDto ExportToFile(List<InputHoldCoInterCompanyResultDto> inputDtos);

        FileDto ExportToFile(List<InputHoldCoResultSummaryDto> inputDtos);

        FileDto ExportToFile(List<InputResultSummaryByStageDto> inputDtos);

    }
}