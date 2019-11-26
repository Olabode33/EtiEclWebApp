using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputatoin.Dtos
{
    public class GetWholesalePdLifetimeOptimisticForEditOutput
    {
		public CreateOrEditWholesalePdLifetimeOptimisticDto WholesalePdLifetimeOptimistic { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}