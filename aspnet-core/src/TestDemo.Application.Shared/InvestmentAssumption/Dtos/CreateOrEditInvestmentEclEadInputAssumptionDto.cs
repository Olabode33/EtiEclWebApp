using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class CreateOrEditInvestmentEclEadInputAssumptionDto : EntityDto<Guid?>
    {

		public string InputName { get; set; }
		
		
		public string Value { get; set; }
		
		
		public bool IsComputed { get; set; }
		
		
		public bool RequiresGroupApproval { get; set; }
		
		
		public bool CanAffiliateEdit { get; set; }
		
		
		 public Guid? InvestmentEclId { get; set; }
		 
		 
    }
}