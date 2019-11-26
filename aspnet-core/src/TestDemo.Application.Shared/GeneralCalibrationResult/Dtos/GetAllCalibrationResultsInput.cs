using Abp.Application.Services.Dto;
using System;

namespace TestDemo.GeneralCalibrationResult.Dtos
{
    public class GetAllCalibrationResultsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}