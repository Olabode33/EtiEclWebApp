using System;
using System.Collections.Generic;
using System.Text;

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
}
