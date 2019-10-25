using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeResults.Dtos
{
    public class GetObeEclResultSummaryKeyInputForEditOutput
    {
		public CreateOrEditObeEclResultSummaryKeyInputDto ObeEclResultSummaryKeyInput { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}