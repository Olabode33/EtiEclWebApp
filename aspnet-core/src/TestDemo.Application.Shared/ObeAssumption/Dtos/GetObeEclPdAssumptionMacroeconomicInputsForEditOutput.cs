using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclPdAssumptionMacroeconomicInputsForEditOutput
    {
		public CreateOrEditObeEclPdAssumptionMacroeconomicInputsDto ObeEclPdAssumptionMacroeconomicInputs { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}