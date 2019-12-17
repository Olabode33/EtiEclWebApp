using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclPdAssumptionNonInternalModelForEditOutput
    {
		public CreateOrEditObeEclPdAssumptionNonInternalModelDto ObeEclPdAssumptionNonInternalModel { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}