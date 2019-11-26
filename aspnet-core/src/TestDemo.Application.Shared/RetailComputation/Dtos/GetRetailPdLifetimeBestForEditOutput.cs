using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailPdLifetimeBestForEditOutput
    {
		public CreateOrEditRetailPdLifetimeBestDto RetailPdLifetimeBest { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}