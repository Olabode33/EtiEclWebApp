using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesalePdLifetimeDownturnForEditOutput
    {
		public CreateOrEditWholesalePdLifetimeDownturnDto WholesalePdLifetimeDownturn { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}