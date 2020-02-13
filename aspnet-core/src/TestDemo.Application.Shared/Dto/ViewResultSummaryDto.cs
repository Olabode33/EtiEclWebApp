using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.Dto
{
    public class ViewEclResultSummaryDto
    {
        public double? TotalExposure { get; set; }
        public double? PreOverrideImpairment { get; set; }
        public double? PreOverrideCoverageRatio { get; set; }
        public double? PostOverrideImpairment { get; set; }
        public double? PostOverrideCoverageRatio { get; set; }
    }
}
