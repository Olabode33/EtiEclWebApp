using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared.Dtos;

namespace TestDemo.EclShared.Importing.Calibration.Dto
{
    public class ImportCalibrationPdCommsConsDto
    {
        public string Customer_No { get; set; }
        public string Account_No { get; set; }
        public string Contract_No { get; set; }
        public string Product_Type { get; set; }
        public int? Current_Rating { get; set; }
        public int? Days_Past_Due { get; set; }
        public string Classification { get; set; }
        public double? Outstanding_Balance_Lcy { get; set; }
        public DateTime? Contract_Start_Date { get; set; }
        public DateTime? Contract_End_Date { get; set; }
        public DateTime? Snapshot_Date { get; set; }
        public string Segment { get; set; }
        public string WI { get; set; }
        public int Serial { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CalibrationId { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }

    public class ImportCalibrationPdCommsConsAsStringDto
    {
        public string Customer_No { get; set; }
        public string Account_No { get; set; }
        public string Contract_No { get; set; }
        public string Product_Type { get; set; }
        public string Days_Past_Due { get; set; }
        public string Classification { get; set; }
        public string Outstanding_Balance_Lcy { get; set; }
        public string Contract_Start_Date { get; set; }
        public string Contract_End_Date { get; set; }
        public string Snapshot_Date { get; set; }
        public string Current_Rating { get; set; }
        public string Segment { get; set; }
        public string WI { get; set; }
        public int Serial { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }

    [Serializable]
    public class SaveCalibrationPdCommsConsFromExcelJobArgs
    {
        public ImportCalibrationDataFromExcelJobArgs Args { get; set; }
        public List<ImportCalibrationPdCommsConsAsStringDto> UploadedRecords { get; set; }
    }
}
