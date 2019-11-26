using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailPdMappingForEditOutput
    {
		public CreateOrEditRetailPdMappingDto RetailPdMapping { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}