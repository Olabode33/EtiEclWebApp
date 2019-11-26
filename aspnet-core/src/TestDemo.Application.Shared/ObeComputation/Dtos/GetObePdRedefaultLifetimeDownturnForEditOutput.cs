using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObePdRedefaultLifetimeDownturnForEditOutput
    {
		public CreateOrEditObePdRedefaultLifetimeDownturnDto ObePdRedefaultLifetimeDownturn { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}