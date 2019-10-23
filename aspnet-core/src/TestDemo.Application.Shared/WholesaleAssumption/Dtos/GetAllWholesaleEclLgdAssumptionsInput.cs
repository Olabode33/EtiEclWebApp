using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetAllWholesaleEclLgdAssumptionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string InputNameFilter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}