using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclPdAssumptionNplIndexForEditOutput
    {
		public CreateOrEditObeEclPdAssumptionNplIndexDto ObeEclPdAssumptionNplIndex { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}