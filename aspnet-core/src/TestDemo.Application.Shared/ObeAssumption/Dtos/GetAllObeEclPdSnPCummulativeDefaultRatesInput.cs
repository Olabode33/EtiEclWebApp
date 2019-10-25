using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetAllObeEclPdSnPCummulativeDefaultRatesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string KeyFilter { get; set; }

		public string RatingFilter { get; set; }

		public int? MaxYearsFilter { get; set; }
		public int? MinYearsFilter { get; set; }

		public double? MaxValueFilter { get; set; }
		public double? MinValueFilter { get; set; }

		public int RequiresGroupApprovalFilter { get; set; }


		 public string ObeEclTenantIdFilter { get; set; }

		 
    }
}