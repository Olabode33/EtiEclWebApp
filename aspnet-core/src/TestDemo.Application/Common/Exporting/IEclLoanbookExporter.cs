using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.Dto.Inputs;

namespace TestDemo.Common.Exporting
{
    public interface IEclLoanbookExporter
    {
        FileDto ExportToFile(List<EclDataLoanBookDto> inputDtos);
    }
}