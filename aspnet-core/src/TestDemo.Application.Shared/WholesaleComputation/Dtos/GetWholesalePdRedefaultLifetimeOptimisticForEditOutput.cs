using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesalePdRedefaultLifetimeOptimisticForEditOutput
    {
		public CreateOrEditWholesalePdRedefaultLifetimeOptimisticDto WholesalePdRedefaultLifetimeOptimistic { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}