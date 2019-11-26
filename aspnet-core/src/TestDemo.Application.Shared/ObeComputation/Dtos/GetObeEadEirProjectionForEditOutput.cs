using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObeEadEirProjectionForEditOutput
    {
		public CreateOrEditObeEadEirProjectionDto ObeEadEirProjection { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}