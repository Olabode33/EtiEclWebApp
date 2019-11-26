using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObeEadInputForEditOutput
    {
		public CreateOrEditObeEadInputDto ObeEadInput { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}