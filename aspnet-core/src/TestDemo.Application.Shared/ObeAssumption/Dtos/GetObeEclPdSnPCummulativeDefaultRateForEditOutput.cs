using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeAssumption.Dtos
{
    public class GetObeEclPdSnPCummulativeDefaultRateForEditOutput
    {
		public CreateOrEditObeEclPdSnPCummulativeDefaultRateDto ObeEclPdSnPCummulativeDefaultRate { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}