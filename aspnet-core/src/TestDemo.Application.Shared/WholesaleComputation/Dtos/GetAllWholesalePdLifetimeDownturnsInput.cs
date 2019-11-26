using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetAllWholesalePdLifetimeDownturnsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 
    }
}