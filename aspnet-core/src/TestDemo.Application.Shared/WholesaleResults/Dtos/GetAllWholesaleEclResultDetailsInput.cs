using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleResults.Dtos
{
    public class GetAllWholesaleEclResultDetailsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string ContractIDFilter { get; set; }

		public string AccountNoFilter { get; set; }

		public string CustomerNoFilter { get; set; }

		public string SegmentFilter { get; set; }

		public string ProductTypeFilter { get; set; }

		public string SectorFilter { get; set; }

		public int? MaxStageFilter { get; set; }
		public int? MinStageFilter { get; set; }

		public double? MaxOutstandingBalanceFilter { get; set; }
		public double? MinOutstandingBalanceFilter { get; set; }

		public double? MaxPreOverrideEclBestFilter { get; set; }
		public double? MinPreOverrideEclBestFilter { get; set; }

		public double? MaxPreOverrideEclOptimisticFilter { get; set; }
		public double? MinPreOverrideEclOptimisticFilter { get; set; }

		public double? MaxPreOverrideEclDownturnFilter { get; set; }
		public double? MinPreOverrideEclDownturnFilter { get; set; }

		public int? MaxOverrideStageFilter { get; set; }
		public int? MinOverrideStageFilter { get; set; }

		public double? MaxOverrideTTRYearsFilter { get; set; }
		public double? MinOverrideTTRYearsFilter { get; set; }

		public double? MaxOverrideFSVFilter { get; set; }
		public double? MinOverrideFSVFilter { get; set; }

		public double? MaxOverrideOverlayFilter { get; set; }
		public double? MinOverrideOverlayFilter { get; set; }

		public double? MaxPostOverrideEclBestFilter { get; set; }
		public double? MinPostOverrideEclBestFilter { get; set; }

		public double? MaxPostOverrideEclOptimisticFilter { get; set; }
		public double? MinPostOverrideEclOptimisticFilter { get; set; }

		public double? MaxPostOverrideEclDownturnFilter { get; set; }
		public double? MinPostOverrideEclDownturnFilter { get; set; }

		public double? MaxPreOverrideImpairmentFilter { get; set; }
		public double? MinPreOverrideImpairmentFilter { get; set; }

		public double? MaxPostOverrideImpairmentFilter { get; set; }
		public double? MinPostOverrideImpairmentFilter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 		 public string WholesaleEclDataLoanBookCustomerNameFilter { get; set; }

		 
    }
}