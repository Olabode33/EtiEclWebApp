using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObePdLifetimeDownturnForEditOutput
    {
		public CreateOrEditObePdLifetimeDownturnDto ObePdLifetimeDownturn { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}