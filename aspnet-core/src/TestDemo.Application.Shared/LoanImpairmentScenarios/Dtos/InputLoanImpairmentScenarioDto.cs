
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.LoanImpairmentScenarios.Dtos
{
    public class InputLoanImpairmentScenarioDto : EntityDto<Guid?>
    {		
		
		public string ScenarioOption { get; set; }
		
		
		public string ApplyOverridesBaseScenario { get; set; }
		
		
		public string ApplyOverridesOptimisticScenario { get; set; }
		
		
		public string ApplyOverridesDownturnScenario { get; set; }
		
		
		public double BestScenarioOverridesValue { get; set; }
		
		
		public double OptimisticScenarioOverridesValue { get; set; }
		
		
		public double DownturnScenarioOverridesValue { get; set; }
		
		
		public double BaseScenario { get; set; }
		
		
		public double OptimisticScenario { get; set; }
		
		

    }
}