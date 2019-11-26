using Abp.Application.Services.Dto;
using System;

namespace TestDemo.LgdCalibrationResult.Dtos
{
    public class GetAllCalibrationResultLgdsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }



    }
}