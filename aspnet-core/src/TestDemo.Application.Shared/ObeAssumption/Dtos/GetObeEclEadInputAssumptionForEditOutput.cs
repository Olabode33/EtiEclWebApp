using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclEadInputAssumptionForEditOutput
    {
		public CreateOrEditObeEclEadInputAssumptionDto ObeEclEadInputAssumption { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}