using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesalePdRedefaultLifetimeForEditOutput
    {
		public CreateOrEditWholesalePdRedefaultLifetimeDto WholesalePdRedefaultLifetime { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}