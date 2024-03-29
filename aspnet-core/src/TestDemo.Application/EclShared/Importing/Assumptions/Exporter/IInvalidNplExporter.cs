﻿using System.Collections.Generic;
using TestDemo.Dto;
using TestDemo.EclShared.Importing.Assumptions.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IInvalidNplExporter
    {
        FileDto ExportToFile(List<ImportNplDataDto> inputDtos);
    }
}