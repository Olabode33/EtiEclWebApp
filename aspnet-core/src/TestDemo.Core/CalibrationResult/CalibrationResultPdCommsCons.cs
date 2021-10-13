using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationResult
{
    [Table("CalibrationResult_Comm_Cons_PD")]
    public class CalibrationResultPdCommsCons : Entity
    {
        public virtual int Month { get; set; }
        public virtual double? Comm_1 { get; set; }
        public virtual double? Cons_1 { get; set; }
        public virtual double? Comm_2 { get; set; }
        public virtual double? Cons_2 { get; set; }
        //public virtual string Comment { get; set; }
        //public virtual int? Status { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual Guid? CalibrationId { get; set; }
    }
}
