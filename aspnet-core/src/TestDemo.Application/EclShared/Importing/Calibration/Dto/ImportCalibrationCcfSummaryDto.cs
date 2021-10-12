using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Importing.Calibration.Dto
{
    public class ImportCalibrationCcfSummaryDto
    {
        public string Customer_No { get; set; }
        public string Account_No { get; set; }
        public string Product_Type { get; set; }
        public string Settlement_Account { get; set; }
        public int? Snapshot_Date { get; set; }
        public DateTime? Contract_Start_Date { get; set; }
        public DateTime? Contract_End_Date { get; set; }
        public double? Limit { get; set; }
        public double? Outstanding_Balance { get; set; }
        public string Classification { get; set; }
        public int Serial { get; set; }
        public Guid? CalibrationId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
