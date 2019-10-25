using Abp.Application.Services.Dto;
using System;

namespace TestDemo.RetailResults.Dtos
{
    public class GetAllRetailEclResultSummaryKeyInputsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string PDGroupingFilter { get; set; }

		public double? MaxExposureFilter { get; set; }
		public double? MinExposureFilter { get; set; }

		public double? MaxCollateralFilter { get; set; }
		public double? MinCollateralFilter { get; set; }

		public double? MaxUnsecuredPercentageFilter { get; set; }
		public double? MinUnsecuredPercentageFilter { get; set; }

		public double? MaxPercentageOfBookFilter { get; set; }
		public double? MinPercentageOfBookFilter { get; set; }

		public double? MaxMonths6CummulativeBestPDsFilter { get; set; }
		public double? MinMonths6CummulativeBestPDsFilter { get; set; }

		public double? MaxMonths12CummulativeBestPDsFilter { get; set; }
		public double? MinMonths12CummulativeBestPDsFilter { get; set; }

		public double? MaxMonths24CummulativeBestPDsFilter { get; set; }
		public double? MinMonths24CummulativeBestPDsFilter { get; set; }


		 public string RetailEclTenantIdFilter { get; set; }

		 
    }
}