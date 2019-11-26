using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetAllRetailLgdContractDatasInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string CONTRACT_NOFilter { get; set; }


		 public string RetailEclTenantIdFilter { get; set; }

		 
    }
}