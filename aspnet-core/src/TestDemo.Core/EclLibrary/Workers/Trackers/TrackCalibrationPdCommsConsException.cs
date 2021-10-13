using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.Workers.Trackers
{
    public class TrackCalibrationPdCommsConsException : Entity
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
        public Guid? CalibrationId { get; set; }
        public string Exception { get; set; }

    }
}
