using Abp.Application.Services.Dto;
using System;

namespace TestDemo.PdCalibrationResult.Dtos
{
    public class GetAllPd12MonthPdsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}