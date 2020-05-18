
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.AffiliateMacroEconomicVariable.Dtos
{
    public class CreateOrEditAffiliateMacroEconomicVariableOffsetDto : EntityDto<int?>
    {

		public int BackwardOffset { get; set; }
		
		
		 public long AffiliateId { get; set; }
		 
		 		 public int MacroeconomicVariableId { get; set; }
		 
		 
    }
}