using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailPdRedefaultLifetimeBestForEditOutput
    {
		public CreateOrEditRetailPdRedefaultLifetimeBestDto RetailPdRedefaultLifetimeBest { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}