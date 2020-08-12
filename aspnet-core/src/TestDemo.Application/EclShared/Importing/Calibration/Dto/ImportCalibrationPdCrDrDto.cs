using System;
using System.Collections.Generic;
using System.Text;
using TestDemo.EclShared.Dtos;

namespace TestDemo.EclShared.Importing.Calibration.Dto
{
    public class ImportCalibrationPdCrDrDto
    {
        public string Customer_No { get; set; }
        public string Account_No { get; set; }
        public string Contract_No { get; set; }
        public string Product_Type { get; set; }
        public int? Days_Past_Due { get; set; }
        public string Classification { get; set; }
        public double? Outstanding_Balance_Lcy { get; set; }
        public DateTime? Contract_Start_Date { get; set; }
        public DateTime? Contract_End_Date { get; set; }
        public int? RAPP_Date { get; set; }
        public string Current_Rating { get; set; }
        public Guid? CalibrationId { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }

    public class ImportCalibrationPdCrDrAsStringDto
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
        public string RAPP_Date { get; set; }
        public string Current_Rating { get; set; }
        public string Exception { get; set; }
        public bool CanBeImported()
        {
            return string.IsNullOrEmpty(Exception);
        }
    }

    [Serializable]
    public class SaveCalibrationPdCrDrFromExcelJobArgs
    {
        public ImportCalibrationDataFromExcelJobArgs Args { get; set; }
        public List<ImportCalibrationPdCrDrAsStringDto> UploadedRecords { get; set; }
    }
}
