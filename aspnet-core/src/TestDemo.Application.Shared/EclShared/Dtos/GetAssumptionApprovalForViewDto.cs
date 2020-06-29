using System;

namespace TestDemo.EclShared.Dtos
{
    public class GetAssumptionApprovalForViewDto
    {
		public AssumptionApprovalDto AssumptionApproval { get; set; }
        public string OrganizationUnitName { get; set; }
		public string UserName { get; set;}
        public string SubmittedBy { get; set; }
        public DateTime DateSubmitted { get; set; }

    }

    public class GetAssumptionApprovalSummaryDto
    {
        public int Submitted { get; set; }
        public int AwaitingApprovals { get; set; }
        public int Approved { get; set; }
    }
}