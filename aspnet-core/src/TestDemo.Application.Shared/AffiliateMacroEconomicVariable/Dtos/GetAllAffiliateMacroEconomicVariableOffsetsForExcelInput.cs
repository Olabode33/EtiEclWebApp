using Abp.Application.Services.Dto;
using System;

namespace TestDemo.AffiliateMacroEconomicVariable.Dtos
{
    public class GetAllAffiliateMacroEconomicVariableOffsetsForExcelInput
    {
		public string Filter { get; set; }

		public int? MaxBackwardOffsetFilter { get; set; }
		public int? MinBackwardOffsetFilter { get; set; }


		 public string OrganizationUnitDisplayNameFilter { get; set; }

		 		 public string MacroeconomicVariableNameFilter { get; set; }

		 
    }
}