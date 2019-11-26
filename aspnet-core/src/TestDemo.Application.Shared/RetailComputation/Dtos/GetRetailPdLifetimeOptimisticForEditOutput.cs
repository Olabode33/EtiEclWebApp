using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailPdLifetimeOptimisticForEditOutput
    {
		public CreateOrEditRetailPdLifetimeOptimisticDto RetailPdLifetimeOptimistic { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}