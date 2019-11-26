using Abp.Application.Services.Dto;
using System;

namespace TestDemo.PdCalibrationResult.Dtos
{
    public class GetAllPdUpperboundsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}