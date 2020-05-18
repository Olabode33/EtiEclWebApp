using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.AffiliateMacroEconomicVariable.Dtos
{
    public class GetAffiliateMacroEconomicVariableOffsetForEditOutput
    {
		public CreateOrEditAffiliateMacroEconomicVariableOffsetDto AffiliateMacroEconomicVariableOffset { get; set; }

		public string OrganizationUnitDisplayName { get; set;}

		public string MacroeconomicVariableName { get; set;}


    }
}