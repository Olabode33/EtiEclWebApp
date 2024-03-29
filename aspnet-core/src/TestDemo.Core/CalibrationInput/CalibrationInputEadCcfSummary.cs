﻿using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationInput
{
    [Table("CalibrationInput_EAD_CCF_Summary")]
    public class CalibrationInputEadCcfSummary: Entity
    {
        public virtual string Customer_No { get; set; }
        public virtual string Account_No { get; set; }
        public virtual string Product_Type { get; set; }
        public virtual string Settlement_Account { get; set; }
        public virtual int? Snapshot_Date { get; set; }
        public virtual DateTime? Contract_Start_Date { get; set; }
        public virtual DateTime? Contract_End_Date { get; set; }
        public virtual double? Limit { get; set; }
        public virtual double? Outstanding_Balance { get; set; }
        public virtual string Classification { get; set; }
        public virtual int Serial { get; set; }
        public virtual Guid? CalibrationId { get; set; }
        public virtual DateTime? DateCreated { get; set; }
    }
}
