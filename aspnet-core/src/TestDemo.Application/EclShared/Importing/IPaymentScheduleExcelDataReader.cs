﻿using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IPaymentScheduleExcelDataReader : ITransientDependency
    {
        List<ImportPaymentScheduleAsStringDto> GetImportPaymentScheduleFromExcel(byte[] fileBytes);
    }
}