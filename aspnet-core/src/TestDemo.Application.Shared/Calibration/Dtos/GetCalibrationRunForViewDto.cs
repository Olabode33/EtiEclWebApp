using System;

namespace TestDemo.Calibration.Dtos
{
    public class GetCalibrationRunForViewDto
    {
		public CalibrationRunDto Calibration { get; set; }
		public string ClosedBy { get; set;}
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
    }
}