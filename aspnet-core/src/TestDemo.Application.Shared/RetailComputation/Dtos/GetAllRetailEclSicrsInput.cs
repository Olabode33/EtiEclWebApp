using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetAllRetailEclSicrsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxComputedSICRFilter { get; set; }
		public int? MinComputedSICRFilter { get; set; }

		public string OverrideSICRFilter { get; set; }

		public string OverrideCommentFilter { get; set; }

		public int StatusFilter { get; set; }


		 public string RetailEclDataLoanBookCustomerNameFilter { get; set; }

		 
    }
}