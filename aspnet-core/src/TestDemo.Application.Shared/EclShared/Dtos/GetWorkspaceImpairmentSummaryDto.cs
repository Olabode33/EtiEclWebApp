using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class GetWorkspaceImpairmentSummaryDto
    {
        public double? TotalExposure { get; set; }
        public double? TotalPreOverride { get; set; }
        public double? TotalPostOverride { get; set; }
        public double? WholesaleExposure { get; set; }
        public double? WholesalePreOverride { get; set; }
        public double? WholesalePostOverride { get; set; }
        public double? RetailExposure { get; set; }
        public double? RetailPreOverride { get; set; }
        public double? RetailPostOverride { get; set; }
        public double? ObeExposure { get; set; }
        public double? ObePreOverride { get; set; }
        public double? ObePostOverride { get; set; }
        public double? InvestmentExposure { get; set; }
        public double? InvestmentPreOverride { get; set; }
        public double? InvestmentPostOverride { get; set; }
    }
}
