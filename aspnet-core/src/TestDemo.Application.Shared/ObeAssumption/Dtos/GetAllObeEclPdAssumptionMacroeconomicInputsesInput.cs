using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetAllObeEclPdAssumptionMacroeconomicInputsesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string ObeEclTenantIdFilter { get; set; }

		 
    }
}