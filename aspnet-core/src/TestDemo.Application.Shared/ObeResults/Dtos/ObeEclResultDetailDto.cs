
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeResults.Dtos
{
    public class ObeEclResultDetailDto : EntityDto<Guid>
    {
		public string ContractID { get; set; }

		public string AccountNo { get; set; }

		public string CustomerNo { get; set; }

		public string Segment { get; set; }

		public string ProductType { get; set; }

		public string Sector { get; set; }

		public int? Stage { get; set; }

		public double? OutstandingBalance { get; set; }

		public double? PreOverrideEclBest { get; set; }

		public double? PreOverrideEclOptimistic { get; set; }

		public double? PreOverrideEclDownturn { get; set; }

		public int? OverrideStage { get; set; }

		public double? OverrideTTRYears { get; set; }

		public double? OverrideFSV { get; set; }

		public double? OverrideOverlay { get; set; }

		public double? PostOverrideEclBest { get; set; }

		public double? PostOverrideEclOptimistic { get; set; }

		public double? PostOverrideEclDownturn { get; set; }

		public double? PreOverrideImpairment { get; set; }

		public double? PostOverrideImpairment { get; set; }


		 public Guid? ObeEclId { get; set; }

		 		 public Guid? ObeEclDataLoanBookId { get; set; }

		 
    }
}