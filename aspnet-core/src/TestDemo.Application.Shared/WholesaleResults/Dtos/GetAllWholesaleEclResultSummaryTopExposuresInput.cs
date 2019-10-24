using Abp.Application.Services.Dto;
using System;

namespace TestDemo.WholesaleResults.Dtos
{
    public class GetAllWholesaleEclResultSummaryTopExposuresInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public double? MaxPreOverrideExposureFilter { get; set; }
		public double? MinPreOverrideExposureFilter { get; set; }

		public double? MaxPreOverrideImpairmentFilter { get; set; }
		public double? MinPreOverrideImpairmentFilter { get; set; }

		public double? MaxPreOverrideCoverageRatioFilter { get; set; }
		public double? MinPreOverrideCoverageRatioFilter { get; set; }

		public double? MaxPostOverrideExposureFilter { get; set; }
		public double? MinPostOverrideExposureFilter { get; set; }

		public double? MaxPostOverrideImpairmentFilter { get; set; }
		public double? MinPostOverrideImpairmentFilter { get; set; }

		public double? MaxPostOverrideCoverageRatioFilter { get; set; }
		public double? MinPostOverrideCoverageRatioFilter { get; set; }

		public string ContractIdFilter { get; set; }


		 public string WholesaleEclTenantIdFilter { get; set; }

		 		 public string WholesaleEclDataLoanBookCustomerNameFilter { get; set; }

		 
    }
}