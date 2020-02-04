using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Dtos
{
    public class GetWorkspaceSummaryDataOutput
    {
        public int AffiliateAssumptionNotUpdatedCount { get; set; }
        public int AffiliateAssumptionYetToBeApprovedCount { get; set; }
        public int DraftEclCount { get; set; }
        public int SubmittedEclCount { get; set; }
        public int WholesaleSubmittedOverrideCount { get; set; }
        public int RetailSubmittedOverrideCount { get; set; }
        public int ObeSubmittedOverrideCount { get; set; }
        public int InvestmentSubmittedOverrideCount { get; set; }
    }
}
