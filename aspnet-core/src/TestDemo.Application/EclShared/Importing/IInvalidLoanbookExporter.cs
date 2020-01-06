using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IInvalidLoanbookExporter
    {
        FileDto ExportToFile(List<ImportLoanbookDto> inputDtos);
    }
}