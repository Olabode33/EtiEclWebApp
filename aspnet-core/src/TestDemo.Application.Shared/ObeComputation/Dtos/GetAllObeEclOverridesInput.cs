using Abp.Application.Services.Dto;
using System;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetAllObeEclOverridesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string ObeEclDataLoanBookCustomerNameFilter { get; set; }

		 
    }
}