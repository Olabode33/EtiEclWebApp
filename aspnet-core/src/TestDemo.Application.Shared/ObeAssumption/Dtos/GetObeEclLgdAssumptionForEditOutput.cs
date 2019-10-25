using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclLgdAssumptionForEditOutput
    {
		public CreateOrEditObeEclLgdAssumptionDto ObeEclLgdAssumption { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}