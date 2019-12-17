using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetAllWholesaleEclPdAssumptionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}