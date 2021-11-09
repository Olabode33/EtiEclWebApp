using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.CalibrationInput
{
    [Table("CalibrationHistory_EAD_Behavioural_Terms")]
    public class CalibrationHistoryEadBehaviouralTerms : Entity
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
        public virtual int Serial { get; set; }
        public virtual DateTime? Restructure_Start_Date { get; set; }
        public virtual DateTime? Restructure_End_Date { get; set; }
        
        public virtual FrameworkEnum? ModelType { get; set; }
        public virtual long? AffiliateId { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual Guid? SourceId { get; set; }
        public virtual SourceTypeEnum? SourceType { get; set; }
        public virtual int? CalibrationSourceId { get; set; }
    }
}
