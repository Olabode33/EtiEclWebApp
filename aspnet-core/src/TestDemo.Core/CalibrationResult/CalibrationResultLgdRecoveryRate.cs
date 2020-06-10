using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.CalibrationResult
{
    [Table("CalibrationResult_LGD_RecoveryRate")]
    public class CalibrationResultLgdRecoveryRate: Entity
    {
        public virtual double? Overall_Exposure_At_Default { get; set; }
        public virtual double? Overall_PvOfAmountReceived { get; set; }
        public virtual double? Overall_Count { get; set; }
        public virtual double? Overall_RecoveryRate { get; set; }
        public virtual double? Corporate_Exposure_At_Default { get; set; }
        public virtual double? Corporate_PvOfAmountReceived { get; set; }
        public virtual double? Corporate_Count { get; set; }
        public virtual double? Corporate_RecoveryRate { get; set; }
        public virtual double? Commercial_Exposure_At_Default { get; set; }
        public virtual double? Commercial_PvOfAmountReceived { get; set; }
        public virtual double? Commercial_Count { get; set; }
        public virtual double? Commercial_RecoveryRate { get; set; }
        public virtual double? Consumer_Exposure_At_Default { get; set; }
        public virtual double? Consumer_PvOfAmountReceived { get; set; }
        public virtual double? Consumer_Count { get; set; }
        public virtual double? Consumer_RecoveryRate { get; set; }
        public virtual string Comment { get; set; }
        public virtual int? Status { get; set; }
        public virtual DateTime? DateCreated { get; set; }
        public virtual Guid? CalibrationId { get; set; }
    }
}
