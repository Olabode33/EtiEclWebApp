using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetAllObeEclAssumptionApprovalsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int AssumptionTypeFilter { get; set; }

		public string OldValueFilter { get; set; }

		public string NewValueFilter { get; set; }

		public DateTime? MaxDateReviewedFilter { get; set; }
		public DateTime? MinDateReviewedFilter { get; set; }

		public string ReviewCommentFilter { get; set; }

		public int StatusFilter { get; set; }

		public int RequiresGroupApprovalFilter { get; set; }


		 public string ObeEclTenantIdFilter { get; set; }

		 		 public string UserNameFilter { get; set; }

		 
    }
}