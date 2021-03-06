﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using TestDemo.Dto;

namespace TestDemo.Calibration.Dtos
{
    public class GetCalibrationRunForEditOutput
    {
		public CreateOrEditCalibrationRunDto Calibration { get; set; }
		public string ClosedByUserName { get; set;}
        public EclAuditInfoDto AuditInfo { get; set; }
        public string AffiliateName { get; set; }
    }
}