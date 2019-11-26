using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailPdRedefaultLifetimeOptimisticForEditOutput
    {
		public CreateOrEditRetailPdRedefaultLifetimeOptimisticDto RetailPdRedefaultLifetimeOptimistic { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}