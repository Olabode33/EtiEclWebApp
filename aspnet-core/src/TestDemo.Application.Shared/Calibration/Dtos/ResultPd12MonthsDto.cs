using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestDemo.Calibration.Dtos
{
    public class ResultPd12MonthsDto : EntityDto
    {
        public int? Rating { get; set; }
        public double? Outstanding_Balance { get; set; }
        public double? Redefault_Balance { get; set; }
        public double? Redefaulted_Balance { get; set; }
        public double? Total_Redefault { get; set; }
        public double? Months_PDs_12 { get; set; }
        public string Comment { get; set; }
        public int? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? CalibrationId { get; set; }
    }
}
