using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObeEadCirProjectionForEditOutput
    {
		public CreateOrEditObeEadCirProjectionDto ObeEadCirProjection { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}