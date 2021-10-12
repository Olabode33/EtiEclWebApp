using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Importing.Calibration.Dto
{
    public class ImportCalibrationLgdHaircutDto
    {
        public string Customer_No { get; set; }
        public string Account_No { get; set; }
        public string Contract_No { get; set; }
        public DateTime? Snapshot_Date { get; set; }
        public int? Period { get; set; }
        public double? Outstanding_Balance_Lcy { get; set; }
        public double? Debenture_OMV { get; set; }
        public double? Debenture_FSV { get; set; }
        public double? Cash_OMV { get; set; }
        public double? Cash_FSV { get; set; }
        public double? Inventory_OMV { get; set; }
        public double? Inventory_FSV { get; set; }
        public double? Plant_And_Equipment_OMV { get; set; }
        public double? Plant_And_Equipment_FSV { get; set; }
        public double? Residential_Property_OMV { get; set; }
        public double? Residential_Property_FSV { get; set; }
        public double? Commercial_Property_OMV { get; set; }
        public double? Commercial_Property_FSV { get; set; }
        public double? Receivables_OMV { get; set; }
        public double? Receivables_FSV { get; set; }
        public double? Shares_OMV { get; set; }
        public double? Shares_FSV { get; set; }
        public double? Vehicle_OMV { get; set; }
        public double? Vehicle_FSV { get; set; }
        public double? Guarantee_Value { get; set; }
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
