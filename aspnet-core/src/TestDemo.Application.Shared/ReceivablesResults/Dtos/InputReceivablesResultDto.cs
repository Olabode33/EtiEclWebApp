
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ReceivablesResults.Dtos
{
    public class InputReceivablesResultDto : EntityDto<Guid?>
    {

		public double TotalExposure { get; set; }
		
		
		public double TotalImpairment { get; set; }
		
		
		public double AdditionalProvision { get; set; }
		
		
		public double Coverage { get; set; }
		
		
		public double OptimisticExposure { get; set; }
		
		
		public double BaseExposure { get; set; }
		
		
		public double DownturnExposure { get; set; }
		
		
		public double ECLTotalExposure { get; set; }
		
		
		public double OptimisticImpairment { get; set; }
		
		
		public double BaseImpairment { get; set; }
		
		
		public double DownturnImpairment { get; set; }
		
		
		public double ECLTotalImpairment { get; set; }
		
		
		public double OptimisticCoverageRatio { get; set; }
		
		
		public double BaseCoverageRatio { get; set; }
		
		
		public double DownturnCoverageRatio { get; set; }
		
		
		public double TotalCoverageRatio { get; set; }		

    }
}