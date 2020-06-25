using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclLibrary.BaseEngine.Dtos
{
    public class UpdatedFacilityStageTrackerDto
    {
        public string Facility { get; set; }
        public int? Stage { get; set; }
        public DateTime? ReportDate { get; set; }
    }
}
