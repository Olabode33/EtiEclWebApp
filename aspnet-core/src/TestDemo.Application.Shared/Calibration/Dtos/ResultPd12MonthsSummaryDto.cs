using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.Calibration.Dtos
{
    public class ResultPd12MonthsSummaryDto : EntityDto
    {
        public double? Normal_12_Months_PD { get; set; }
        public double? DefaultedLoansA { get; set; }
        public double? DefaultedLoansB { get; set; }
        public double? CuredLoansA { get; set; }
        public double? CuredLoansB { get; set; }
        public double? Cure_Rate { get; set; }
        public double? CuredPopulationA { get; set; }
        public double? CuredPopulationB { get; set; }
        public double? RedefaultedLoansA { get; set; }
        public double? RedefaultedLoansB { get; set; }
        public double? Redefault_Rate { get; set; }
        public double? Redefault_Factor { get; set; }
        public double? Commercial_CureRate { get; set; }
        public double? Commercial_RedefaultRate { get; set; }
        public double? Consumer_CureRate { get; set; }
        public double? Consumer_RedefaultRate { get; set; }
        public string Comment { get; set; }
        public int? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CalibrationId { get; set; }
    }

    public class GetAllResultPdCrDrDto
    {
        public List<ResultPd12MonthsDto> Pd12Months { get; set; }
        public ResultPd12MonthsSummaryDto Pd12MonthsSummary { get; set; }
        public List<ResultPdCommsConsDto> PdCommsCons { get; set; }
    }
}
