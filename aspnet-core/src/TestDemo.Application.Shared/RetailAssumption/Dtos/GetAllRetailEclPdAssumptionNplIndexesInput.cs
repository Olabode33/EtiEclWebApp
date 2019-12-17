using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetAllRetailEclPdAssumptionNplIndexesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string RetailEclTenantIdFilter { get; set; }

		 
    }
}