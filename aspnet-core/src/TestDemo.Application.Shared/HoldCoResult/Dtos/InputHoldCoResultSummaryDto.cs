
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.HoldCoResult.Dtos
{
    public class InputHoldCoResultSummaryDto : EntityDto<Guid?>
    {

		public double BestEstimateExposure { get; set; }
		
		
		public double OptimisticExposure { get; set; }
		
		
		public double DownturnExposure { get; set; }
		
		
		public double BestEstimateTotal { get; set; }
		
		
		public double OptimisticTotal { get; set; }
		
		
		public string DownturnTotal { get; set; }
		
		
		public double BestEstimateImpairmentRatio { get; set; }
		
		
		public double OptimisticImpairmentRatio { get; set; }
		
		
		public double DownturnImpairmentRatio { get; set; }
		
		
		public double Exposure { get; set; }
		
		
		public double Total { get; set; }
		
		
		public double ImpairmentRatio { get; set; }
		
		
		public bool Check { get; set; }
		
		
		public string Diff { get; set; }	
		

    }
}