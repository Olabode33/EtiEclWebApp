using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationResult
{
    [Table("CalibrationResult_PD_12Months")]
    public class CalibrationResultPd12Months: Entity
    {
        public virtual int? Rating { get; set; }
        public virtual double? Outstanding_Balance { get; set; }
        public virtual double? Redefault_Balance { get; set; }
        public virtual double? Redefaulted_Balance { get; set; }
        public virtual double? Total_Redefault { get; set; }
        public virtual double? Months_PDs_12 { get; set; }
        public virtual string Comment { get; set; }
        public virtual int? Status { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual Guid? CalibrationId { get; set; }
    }
}
