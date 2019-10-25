using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;

namespace TestDemo.RetailResults.Dtos
{
    public class RetailEclResultSummaryDto : EntityDto<Guid>
    {
		public ResultSummaryTypeEnum SummaryType { get; set; }

		public string Title { get; set; }

		public double? PreOverrideExposure { get; set; }

		public double? PreOverrideImpairment { get; set; }

		public double? PreOverrideCoverageRatio { get; set; }

		public double? PostOverrideExposure { get; set; }

		public double? PostOverrideImpairment { get; set; }

		public double? PostOverrideCoverageRatio { get; set; }


		 public Guid? RetailEclId { get; set; }

		 
    }
}