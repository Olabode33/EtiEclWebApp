using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetAllRetailEadCirProjectionsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string RetailEclTenantIdFilter { get; set; }

		 
    }
}