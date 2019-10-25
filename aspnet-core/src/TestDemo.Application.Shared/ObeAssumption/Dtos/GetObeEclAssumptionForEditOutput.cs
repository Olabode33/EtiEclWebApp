using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclAssumptionForEditOutput
    {
		public CreateOrEditObeEclAssumptionDto ObeEclAssumption { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}