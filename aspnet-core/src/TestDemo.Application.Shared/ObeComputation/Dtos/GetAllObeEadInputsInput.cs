using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetAllObeEadInputsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string ObeEclTenantIdFilter { get; set; }

		 
    }
}