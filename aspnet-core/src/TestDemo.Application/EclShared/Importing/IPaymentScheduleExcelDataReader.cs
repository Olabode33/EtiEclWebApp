using Abp.Dependency;
using System.Collections.Generic;
using TestDemo.EclShared.Importing.Dto;

namespace TestDemo.EclShared.Importing
{
    public interface IPaymentScheduleExcelDataReader : ITransientDependency
    {
        List<ImportPaymentScheduleDto> GetImportPaymentScheduleFromExcel(byte[] fileBytes);
    }
}