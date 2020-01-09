using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetAllRetailEclOverridesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string RetailEclDataLoanBookCustomerNameFilter { get; set; }

		 
    }
}