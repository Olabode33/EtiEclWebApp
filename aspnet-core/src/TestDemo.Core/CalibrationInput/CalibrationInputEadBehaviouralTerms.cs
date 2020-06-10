using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationInput
{
    [Table("CalibrationInput_EAD_Behavioural_Terms")]
    [Audited]
    public class CalibrationInputEadBehaviouralTerms: Entity
    {
        public virtual string Customer_No { get; set; }
        public virtual string Account_No { get; set; }
        public virtual string Contract_No { get; set; }
        public virtual string Customer_Name { get; set; }
        public virtual DateTime? Snapshot_Date { get; set; }
        public virtual string Classification { get; set; }
        public virtual double? Original_Balance_Lcy { get; set; }
        public virtual double? Outstanding_Balance_Lcy { get; set; }
        public virtual double? Outstanding_Balance_Acy { get; set; }
        public virtual DateTime? Contract_Start_Date { get; set; }
        public virtual DateTime? Contract_End_Date { get; set; }
        public virtual string Restructure_Indicator { get; set; }
        public virtual string Restructure_Type { get; set; }
        public virtual DateTime? Restructure_Start_Date { get; set; }
        public virtual DateTime? Restructure_End_Date { get; set; }
        public virtual Guid? CalibrationId { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual string Assumption_NonExpired { get; set; }
        public virtual string Freq_NonExpired { get; set; }
        public virtual string Assumption_Expired { get; set; }
        public virtual string Freq_Expired { get; set; }
        public virtual string Comment { get; set; }
    }
}
