using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetAllWholesalePdRedefaultLifetimeOptimisticsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}