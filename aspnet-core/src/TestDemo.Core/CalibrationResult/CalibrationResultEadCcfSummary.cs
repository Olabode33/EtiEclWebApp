using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationResult
{
    [Table("CalibrationResult_EAD_CCF_Summary")]
    public class CalibrationResultEadCcfSummary: Entity
    {
        public virtual double? OD_TotalLimitOdDefaultedLoan { get; set; }
        public virtual double? OD_BalanceAtDefault { get; set; }
        public virtual double? OD_Balance12MonthBeforeDefault { get; set; }
        public virtual double? OD_TotalConversation { get; set; }
        public virtual double? OD_CCF { get; set; }
        public virtual double? Card_TotalLimitOdDefaultedLoan { get; set; }
        public virtual double? Card_BalanceAtDefault { get; set; }
        public virtual double? Card_Balance12MonthBeforeDefault { get; set; }
        public virtual double? Card_TotalConversation { get; set; }
        public virtual double? Card_CCF { get; set; }
        public virtual double? Overall_TotalLimitOdDefaultedLoan { get; set; }
        public virtual double? Overall_BalanceAtDefault { get; set; }
        public virtual double? Overall_Balance12MonthBeforeDefault { get; set; }
        public virtual double? Overall_TotalConversation { get; set; }
        public virtual double? Overall_CCF { get; set; }
        public virtual string Comment { get; set; }
        public virtual int? Status { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual Guid? CalibrationId { get; set; }
    }
}
