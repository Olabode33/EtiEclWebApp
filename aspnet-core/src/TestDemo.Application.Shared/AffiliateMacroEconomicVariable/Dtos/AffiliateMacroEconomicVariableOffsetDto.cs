
using System;
using Abp.Application.Services.Dto;

namespace TestDemo.AffiliateMacroEconomicVariable.Dtos
{
    public class AffiliateMacroEconomicVariableOffsetDto : EntityDto
    {
		public int BackwardOffset { get; set; }


		 public long AffiliateId { get; set; }

		 		 public int MacroeconomicVariableId { get; set; }

		 
    }
}