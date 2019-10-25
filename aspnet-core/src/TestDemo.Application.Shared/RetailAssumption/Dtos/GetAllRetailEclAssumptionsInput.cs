using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetAllRetailEclAssumptionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string KeyFilter { get; set; }

		public string InputNameFilter { get; set; }

		public string ValueFilter { get; set; }

		public int DatatypeFilter { get; set; }

		public int IsComputedFilter { get; set; }

		public int AssumptionGroupFilter { get; set; }

		public int RequiresGroupApprovalFilter { get; set; }


		 public string RetailEclTenantIdFilter { get; set; }

		 
    }
}