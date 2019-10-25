using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetAllRetailEclComputedEadResultsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string LifetimeEADFilter { get; set; }


		 public string RetailEclDataLoanBookCustomerNameFilter { get; set; }

		 
    }
}