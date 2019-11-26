using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleComputatoin.Dtos
{
    public class GetAllWholesalePdLifetimeOptimisticsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}