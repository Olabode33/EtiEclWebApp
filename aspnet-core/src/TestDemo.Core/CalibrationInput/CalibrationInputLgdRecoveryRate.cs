using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationInput
{
    [Table("CalibrationInput_LGD_RecoveryRate")]
    public class CalibrationInputLgdRecoveryRate: EntityDto
    {
        public virtual string Customer_No { get; set; }
        public virtual string Account_No { get; set; }
        public virtual string Account_Name { get; set; }
        public virtual string Contract_No { get; set; }
        public virtual string Segment { get; set; }
        public virtual int? Days_Past_Due { get; set; }
        public virtual string Classification { get; set; }
        public virtual DateTime? Default_Date { get; set; }
        public virtual double? Outstanding_Balance_Lcy { get; set; }
        public virtual double? Contractual_Interest_Rate { get; set; }
        public virtual double? Amount_Recovered { get; set; }
        public virtual DateTime? Date_Of_Recovery { get; set; }
        public virtual string Type_Of_Recovery { get; set; }
        public virtual Guid? CalibrationId { get; set; }
        public virtual DateTime? DateCreated { get; set; }
    }
}
