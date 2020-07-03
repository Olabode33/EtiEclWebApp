using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared.Dtos;

namespace TestDemo.EclShared.Importing.Dto
{
    public class ImportPaymentScheduleDto
    {
        public string ContractRefNo { get; set; }

        public DateTime? StartDate { get; set; }

        public string Component { get; set; }

        public int? NoOfSchedules { get; set; }

        public string Frequency { get; set; }

        public double? Amount { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }

    public class ImportPaymentScheduleAsStringDto
    {
        public string ContractRefNo { get; set; }
        public string StartDate { get; set; }
        public string Component { get; set; }
        public string NoOfSchedules { get; set; }
        public string Frequency { get; set; }
        public string Amount { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }

    [Serializable]
    public class SaveEclPaymentScheduleDataFromExcelJobArgs
    {
        public ImportEclDataFromExcelJobArgs Args { get; set; }
        public List<ImportPaymentScheduleAsStringDto> PaymentSchedules { get; set; }
    }
}
