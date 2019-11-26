using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObePdRedefaultLifetimeOptimisticForEditOutput
    {
		public CreateOrEditObePdRedefaultLifetimeOptimisticDto ObePdRedefaultLifetimeOptimistic { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}