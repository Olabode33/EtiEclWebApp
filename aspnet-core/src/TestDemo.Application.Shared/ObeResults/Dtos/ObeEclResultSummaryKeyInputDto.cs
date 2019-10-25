
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.ObeResults.Dtos
{
    public class ObeEclResultSummaryKeyInputDto : EntityDto<Guid>
    {
		public string PDGrouping { get; set; }

		public double? Exposure { get; set; }

		public double? Collateral { get; set; }

		public double? UnsecuredPercentage { get; set; }

		public double? PercentageOfBook { get; set; }

		public double? Months6CummulativeBestPDs { get; set; }

		public double? Months12CummulativeBestPDs { get; set; }

		public double? Months24CummulativeBestPDs { get; set; }


		 public Guid? ObeEclId { get; set; }

		 
    }
}