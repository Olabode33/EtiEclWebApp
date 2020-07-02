using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.Calibration.Dtos
{
    public class ResultEadCcfSummaryDto : EntityDto
    {
        public double? OD_TotalLimitOdDefaultedLoan { get; set; }
        public double? OD_BalanceAtDefault { get; set; }
        public double? OD_Balance12MonthBeforeDefault { get; set; }
        public double? OD_TotalConversation { get; set; }
        public double? OD_CCF { get; set; }
        public double? Card_TotalLimitOdDefaultedLoan { get; set; }
        public double? Card_BalanceAtDefault { get; set; }
        public double? Card_Balance12MonthBeforeDefault { get; set; }
        public double? Card_TotalConversation { get; set; }
        public double? Card_CCF { get; set; }
        public double? Overall_TotalLimitOdDefaultedLoan { get; set; }
        public double? Overall_BalanceAtDefault { get; set; }
        public double? Overall_Balance12MonthBeforeDefault { get; set; }
        public double? Overall_TotalConversation { get; set; }
        public double? Overall_CCF { get; set; }
        public string Comment { get; set; }
        public int? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CalibrationId { get; set; }
        public int Id { get; set; }

    }
}
