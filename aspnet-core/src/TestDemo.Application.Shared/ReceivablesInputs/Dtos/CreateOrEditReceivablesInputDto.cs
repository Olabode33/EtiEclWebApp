
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ReceivablesInputs.Dtos
{
    public class CreateOrEditReceivablesInputDto : EntityDto<Guid?>
    {

		public DateTime ReportingDate { get; set; }
		
		
		public double ScenarioOptimistic { get; set; }
		
		
		public int LossDefinition { get; set; }
		
		
		public double LossRate { get; set; }
		
		
		public bool FLIOverlay { get; set; }
		
		
		public double OverlayOptimistic { get; set; }
		
		
		public double OverlayBase { get; set; }
		
		
		public double OverlayDownturn { get; set; }
		
		
		public double InterceptCoefficient { get; set; }
		
		
		public double IndexCoefficient { get; set; }
		
		
		public double LossRateCoefficient { get; set; }
		
		
		public Guid RegisterId { get; set; }
		
		
		public double ScenarioBase { get; set; }
		
		

    }
}