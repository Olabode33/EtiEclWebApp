using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetAllObeEclComputedEadResultsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string LifetimeEADFilter { get; set; }


		 public string ObeEclDataLoanBookContractNoFilter { get; set; }

		 
    }
}