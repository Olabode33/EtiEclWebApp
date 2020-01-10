using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class CreateOrEditInvestmentPdInputMacroEconomicAssumptionDto : EntityDto<Guid?>
    {

		public string Key { get; set; }

        public int Month { get; set; }
		
		
		public double BestValue { get; set; }
		
		
		public double OptimisticValue { get; set; }
		
		
		public double DownturnValue { get; set; }
		
		
		public bool CanAffiliateEdit { get; set; }
		
		
		public bool RequiresGroupApproval { get; set; }
		
		
		 public Guid InvestmentEclId { get; set; }
		 
		 
    }
}