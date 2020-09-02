using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Calibration.Dto;
using TestDemo.HoldCoAssetBook.Dtos;
using TestDemo.IVModels.Dtos;

namespace TestDemo.Calibration.Exporting
{
    public interface IInputPdCrDrExporter
    {
        FileDto ExportToFile(List<InputPdCrDrDto> inputDtos);

        FileDto ExportToFile(List<InputMacroEconomicCreditIndexDto> inputDtos);

        FileDto ExportToFile(List<InputAssetBookDto> inputDtos);


    }
}