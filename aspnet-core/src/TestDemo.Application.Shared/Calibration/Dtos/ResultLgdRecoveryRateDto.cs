using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.Calibration.Dtos
{
    public class ResultLgdRecoveryRateDto : EntityDto
    {
        public double? Overall_Exposure_At_Default { get; set; }
        public double? Overall_PvOfAmountReceived { get; set; }
        public double? Overall_Count { get; set; }
        public double? Overall_RecoveryRate { get; set; }
        public double? Corporate_Exposure_At_Default { get; set; }
        public double? Corporate_PvOfAmountReceived { get; set; }
        public double? Corporate_Count { get; set; }
        public double? Corporate_RecoveryRate { get; set; }
        public double? Commercial_Exposure_At_Default { get; set; }
        public double? Commercial_PvOfAmountReceived { get; set; }
        public double? Commercial_Count { get; set; }
        public double? Commercial_RecoveryRate { get; set; }
        public double? Consumer_Exposure_At_Default { get; set; }
        public double? Consumer_PvOfAmountReceived { get; set; }
        public double? Consumer_Count { get; set; }
        public double? Consumer_RecoveryRate { get; set; }
        public string Comment { get; set; }
        public int? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CalibrationId { get; set; }
    }
}
