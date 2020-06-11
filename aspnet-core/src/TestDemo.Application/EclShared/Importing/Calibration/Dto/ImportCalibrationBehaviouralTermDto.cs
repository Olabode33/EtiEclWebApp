using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Importing.Calibration.Dto
{
    public class ImportCalibrationBehaviouralTermDto
    {
        public string Customer_No { get; set; }
        public string Account_No { get; set; }
        public string Contract_No { get; set; }
        public string Customer_Name { get; set; }
        public DateTime? Snapshot_Date { get; set; }
        public string Classification { get; set; }
        public double? Original_Balance_Lcy { get; set; }
        public double? Outstanding_Balance_Lcy { get; set; }
        public double? Outstanding_Balance_Acy { get; set; }
        public DateTime? Contract_Start_Date { get; set; }
        public DateTime? Contract_End_Date { get; set; }
        public string Restructure_Indicator { get; set; }
        public string Restructure_Type { get; set; }
        public DateTime? Restructure_Start_Date { get; set; }
        public DateTime? Restructure_End_Date { get; set; }
        public Guid? CalibrationId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Assumption_NonExpired { get; set; }
        public string Freq_NonExpired { get; set; }
        public string Assumption_Expired { get; set; }
        public string Freq_Expired { get; set; }
        public string Comment { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }
}
