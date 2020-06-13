using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.Dto.Inputs;

namespace TestDemo.Common.Exporting
{
    public interface IEclDataAssetBookExporter
    {
        FileDto ExportToFile(List<EclDataAssetBookDto> inputDtos);
    }
}