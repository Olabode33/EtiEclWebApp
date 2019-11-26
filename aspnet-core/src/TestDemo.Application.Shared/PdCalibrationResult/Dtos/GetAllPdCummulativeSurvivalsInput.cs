using Abp.Application.Services.Dto;
using System;

namespace TestDemo.PdCalibrationResult.Dtos
{
    public class GetAllPdCummulativeSurvivalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}