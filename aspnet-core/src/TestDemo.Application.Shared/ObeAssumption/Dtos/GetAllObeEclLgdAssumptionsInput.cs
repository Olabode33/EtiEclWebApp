using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetAllObeEclLgdAssumptionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string KeyFilter { get; set; }

		public string InputNameFilter { get; set; }

		public string ValueFilter { get; set; }

		public int DataTypeFilter { get; set; }

		public int IsComputedFilter { get; set; }

		public int LgdGroupFilter { get; set; }

		public int RequiresGroupApprovalFilter { get; set; }


		 public string ObeEclTenantIdFilter { get; set; }

		 
    }
}