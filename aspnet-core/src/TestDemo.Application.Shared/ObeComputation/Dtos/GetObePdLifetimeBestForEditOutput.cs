using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObePdLifetimeBestForEditOutput
    {
		public CreateOrEditObePdLifetimeBestDto ObePdLifetimeBest { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}