using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObePdMappingForEditOutput
    {
		public CreateOrEditObePdMappingDto ObePdMapping { get; set; }

		public string ObeEclTenantId { get; set;}


    }
}