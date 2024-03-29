﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.Workers.Trackers
{
    public class TrackCalibrationBehaviouralTermException : Entity
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
        public string Exception { get; set; }

    }
}
