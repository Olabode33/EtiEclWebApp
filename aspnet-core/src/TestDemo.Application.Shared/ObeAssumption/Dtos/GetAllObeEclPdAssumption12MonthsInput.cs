using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetAllObeEclPdAssumption12MonthsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxCreditFilter { get; set; }
		public int? MinCreditFilter { get; set; }

		public double? MaxPDFilter { get; set; }
		public double? MinPDFilter { get; set; }

		public string SnPMappingEtiCreditPolicyFilter { get; set; }

		public string SnPMappingBestFitFilter { get; set; }

		public int RequiresGroupApprovalFilter { get; set; }


		 public string ObeEclTenantIdFilter { get; set; }

		 
    }
}