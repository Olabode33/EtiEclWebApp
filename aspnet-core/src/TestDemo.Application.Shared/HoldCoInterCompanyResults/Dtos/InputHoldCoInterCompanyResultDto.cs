
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.HoldCoInterCompanyResults.Dtos
{
    public class InputHoldCoInterCompanyResultDto : EntityDto<Guid?>
    {		
		
		public string AssetType { get; set; }
		
		
		public string AssetDescription { get; set; }
		
		
		public int Stage { get; set; }
		
		
		public double OutstandingBalance { get; set; }
		
		
		public double BestEstimate { get; set; }
		
		
		public double Optimistic { get; set; }
		
		
		public double Downturn { get; set; }
		
		
		public double Impairment { get; set; }
		
		

    }
}