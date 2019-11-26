using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetAllObePdLifetimeDownturnsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string ObeEclTenantIdFilter { get; set; }

		 
    }
}