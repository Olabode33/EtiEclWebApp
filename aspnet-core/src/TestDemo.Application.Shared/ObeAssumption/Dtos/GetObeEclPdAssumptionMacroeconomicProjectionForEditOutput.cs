using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclPdAssumptionMacroeconomicProjectionForEditOutput
    {
		public CreateOrEditObeEclPdAssumptionMacroeconomicProjectionDto ObeEclPdAssumptionMacroeconomicProjection { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}