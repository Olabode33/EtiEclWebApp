using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclPdAssumptionForEditOutput
    {
		public CreateOrEditObeEclPdAssumptionDto ObeEclPdAssumption { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}