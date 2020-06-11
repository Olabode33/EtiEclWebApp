using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Calibration.Dtos
{
    public class CalibrationInputSummaryDto<T>
    {
        public int Total { get; set; }
        public List<T> Items { get; set; }
    }
}
