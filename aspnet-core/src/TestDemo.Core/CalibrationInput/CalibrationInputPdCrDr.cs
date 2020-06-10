﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationInput
{
    [Table("CalibrationInput_PD_CR_DR")]
    public class CalibrationInputPdCrDr: EntityDto
    {
        public virtual string Customer_No { get; set; }
        public virtual string Account_No { get; set; }
        public virtual string Contract_No { get; set; }
        public virtual string Product_Type { get; set; }
        public virtual int? Days_Past_Due { get; set; }
        public virtual string Classification { get; set; }
        public virtual double? Outstanding_Balance_Lcy { get; set; }
        public virtual DateTime? Contract_Start_Date { get; set; }
        public virtual DateTime? Contract_End_Date { get; set; }
        public virtual DateTime? RAPP_Date { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual Guid? CalibrationId { get; set; }
    }
}
