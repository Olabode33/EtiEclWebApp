
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.HoldCoResult.Dtos
{
    public class CreateOrEditResultSummaryByStageDto : EntityDto<Guid?>
    {

		public double StageOneExposure { get; set; }
		
		
		public double StageTwoExposure { get; set; }
		
		
		public double StageThreeExposure { get; set; }
		
		
		public double TotalExposure { get; set; }
		
		
		public double StageOneImpairment { get; set; }
		
		
		public double StageTwoImpairment { get; set; }
		
		
		public double StageThreeImpairment { get; set; }
		
		
		public double StageOneImpairmentRatio { get; set; }
		
		
		public double StageTwoImpairmentRatio { get; set; }
		
		
		public double TotalImpairment { get; set; }
		
		
		public double StageThreeImpairmentRatio { get; set; }
		
		
		public double TotalImpairmentRatio { get; set; }
		
		
		public Guid RegistrationId { get; set; }
		
		

    }
}