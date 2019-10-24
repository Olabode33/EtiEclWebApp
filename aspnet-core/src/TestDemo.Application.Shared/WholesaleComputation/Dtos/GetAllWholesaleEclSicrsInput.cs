using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetAllWholesaleEclSicrsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public int? MaxComputedSICRFilter { get; set; }
		public int? MinComputedSICRFilter { get; set; }

		public string OverrideSICRFilter { get; set; }

		public string OverrideCommentFilter { get; set; }

		public int StatusFilter { get; set; }


		 public string WholesaleEclDataLoanBookContractNoFilter { get; set; }

		 
    }
}