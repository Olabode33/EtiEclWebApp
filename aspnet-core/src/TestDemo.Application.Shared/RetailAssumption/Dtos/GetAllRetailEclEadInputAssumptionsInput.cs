using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetAllRetailEclEadInputAssumptionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string KeyFilter { get; set; }

		public string InputNameFilter { get; set; }

		public string ValueFilter { get; set; }

		public int DatatypeFilter { get; set; }

		public int IsComputedFilter { get; set; }

		public int EadGroupFilter { get; set; }

		public int RequiresGroupApprovalFilter { get; set; }


		 public string RetailEclTenantIdFilter { get; set; }

		 
    }
}