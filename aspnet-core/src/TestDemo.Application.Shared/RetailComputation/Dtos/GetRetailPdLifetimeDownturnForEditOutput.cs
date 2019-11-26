using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailPdLifetimeDownturnForEditOutput
    {
		public CreateOrEditRetailPdLifetimeDownturnDto RetailPdLifetimeDownturn { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}