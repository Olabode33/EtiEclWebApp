using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.Calibration.Dtos
{
    public class GetCalibrationRunForEditOutput
    {
		public CreateOrEditCalibrationRunDto Calibration { get; set; }

		public string UserName { get; set;}


    }
}