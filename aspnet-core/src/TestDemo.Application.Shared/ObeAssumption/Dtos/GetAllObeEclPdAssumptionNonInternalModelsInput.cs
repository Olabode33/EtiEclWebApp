using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetAllObeEclPdAssumptionNonInternalModelsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string ObeEclTenantIdFilter { get; set; }

		 
    }
}