using Abp.Application.Services.Dto;
using System;

namespace TestDemo.Calibration.Dtos
{
    public class GetAllCalibrationRunInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public int? StatusFilter { get; set; }
        public string UserNameFilter { get; set; }
    }
}