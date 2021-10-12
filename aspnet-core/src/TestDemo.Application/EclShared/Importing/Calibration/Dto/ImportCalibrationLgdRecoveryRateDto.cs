using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Importing.Calibration.Dto
{
    public class ImportCalibrationLgdRecoveryRateDto
    {
        public string Customer_No { get; set; }
        public string Account_No { get; set; }
        public string Account_Name { get; set; }
        public string Contract_No { get; set; }
        public string Segment { get; set; }
        public string Product_Type { get; set; }
        public int? Days_Past_Due { get; set; }
        public string Classification { get; set; }
        public DateTime? Default_Date { get; set; }
        public double? Outstanding_Balance_Lcy { get; set; }
        public double? Contractual_Interest_Rate { get; set; }
        public double? Amount_Recovered { get; set; }
        public DateTime? Date_Of_Recovery { get; set; }
        public string Type_Of_Recovery { get; set; }
        public Guid? CalibrationId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Exception { get; set; }
        public int Serial { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
