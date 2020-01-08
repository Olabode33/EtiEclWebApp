using TestDemo.EclShared;
using TestDemo.EclShared;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class CreateOrEditInvestmentEclLgdInputAssumptionDto : EntityDto<Guid?>
    {

		public string Value { get; set; }
		
		
		public bool IsComputed { get; set; }
		
		
		public bool RequiresGroupApproval { get; set; }
		
		
		public bool CanAffiliateEdit { get; set; }
		
		
		 public Guid InvestmentEclId { get; set; }
		 
		 
    }
}