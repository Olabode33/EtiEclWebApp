using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestDemo.EclShared;

namespace TestDemo.CalibrationInput
{
    [Table("CalibrationHistory_LGD_RecoveryRate")]
    public class CalibrationHistoryLgdRecoveryRate : Entity
    {
        public virtual string Customer_No { get; set; }
        public virtual string Account_No { get; set; }
        public virtual string Account_Name { get; set; }
        public virtual string Contract_No { get; set; }
        public virtual string Segment { get; set; }
        public virtual string Product_Type { get; set; }
        public virtual int? Days_Past_Due { get; set; }
        public virtual string Classification { get; set; }
        public virtual DateTime? Default_Date { get; set; }
        public virtual double? Outstanding_Balance_Lcy { get; set; }
        public virtual double? Contractual_Interest_Rate { get; set; }
        public virtual double? Amount_Recovered { get; set; }
        public virtual DateTime? Date_Of_Recovery { get; set; }
        public virtual string Type_Of_Recovery { get; set; }
        public virtual FrameworkEnum? ModelType { get; set; }
        public virtual long? AffiliateId { get; set; }
        public virtual DateTime? DateCreated { get; set; }
    }
}
