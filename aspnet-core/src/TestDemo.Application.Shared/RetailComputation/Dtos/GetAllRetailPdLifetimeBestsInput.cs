using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetAllRetailPdLifetimeBestsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string RetailEclTenantIdFilter { get; set; }

		 
    }
}