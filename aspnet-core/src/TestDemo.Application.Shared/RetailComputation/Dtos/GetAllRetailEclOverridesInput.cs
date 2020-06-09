using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetAllRetailEclOverridesInput : PagedAndSortedResultRequestDto
    {
		public Guid EclId { get; set; }
		public string Filter { get; set; }
		public int StatusFilter { get; set; }
		public string RetailEclDataLoanBookCustomerNameFilter { get; set; }

		 
    }
}