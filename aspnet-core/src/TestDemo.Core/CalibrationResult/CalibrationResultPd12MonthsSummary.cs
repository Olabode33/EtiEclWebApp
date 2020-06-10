using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationResult
{
    [Table("CalibrationResult_PD_12Months_Summary")]
    public class CalibrationResultPd12MonthsSummary: EntityDto
    {
        public virtual double? Normal_12_Months_PD { get; set; }
        public virtual double? DefaultedLoansA { get; set; }
        public virtual double? DefaultedLoansB { get; set; }
        public virtual double? CuredLoansA { get; set; }
        public virtual double? CuredLoansB { get; set; }
        public virtual double? Cure_Rate { get; set; }
        public virtual double? CuredPopulationA { get; set; }
        public virtual double? CuredPopulationB { get; set; }
        public virtual double? RedefaultedLoansA { get; set; }
        public virtual double? RedefaultedLoansB { get; set; }
        public virtual double? Redefault_Rate { get; set; }
        public virtual double? Redefault_Factor { get; set; }
        public virtual string Comment { get; set; }
        public virtual int? Status { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual Guid? CalibrationId { get; set; }
    }
}
