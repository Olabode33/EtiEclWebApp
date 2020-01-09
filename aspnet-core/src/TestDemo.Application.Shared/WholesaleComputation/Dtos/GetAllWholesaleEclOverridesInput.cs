using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetAllWholesaleEclOverridesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string WholesaleEclDataLoanBookCustomerNameFilter { get; set; }

		 
    }
}