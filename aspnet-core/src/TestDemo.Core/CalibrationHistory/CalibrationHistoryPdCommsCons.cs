﻿using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.CalibrationInput
{
    [Table("CalibrationHistory_Comm_Cons_PD")]
    public class CalibrationHistoryPdCommsCons : Entity
    {
        public virtual string Customer_No { get; set; }
        public virtual string Account_No { get; set; }
        public virtual string Contract_No { get; set; }
        public virtual string Product_Type { get; set; }
        public virtual int? Current_Rating { get; set; }
        public virtual int? Days_Past_Due { get; set; }
        public virtual string Classification { get; set; }
        public virtual double? Outstanding_Balance_Lcy { get; set; }
        public virtual DateTime? Contract_Start_Date { get; set; }
        public virtual DateTime? Contract_End_Date { get; set; }
        public virtual int? Snapshot_Date { get; set; }
        public virtual string Segment { get; set; }
        public virtual int Serial { get; set; }
        public virtual string WI { get; set; }
        public virtual FrameworkEnum? ModelType { get; set; }
        // public virtual int Serial { get; set; }
        public virtual long? AffiliateId { get; set; }
        public virtual DateTime? DateCreated { get; set; }
		        public virtual Guid? SourceId { get; set; }
        public virtual SourceTypeEnum? SourceType { get; set; }
        public virtual int? CalibrationSourceId { get; set; }
    }
}