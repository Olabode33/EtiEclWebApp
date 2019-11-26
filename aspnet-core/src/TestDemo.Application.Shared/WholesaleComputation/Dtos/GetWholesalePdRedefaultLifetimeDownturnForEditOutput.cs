using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesalePdRedefaultLifetimeDownturnForEditOutput
    {
		public CreateOrEditWholesalePdRedefaultLifetimeDownturnDto WholesalePdRedefaultLifetimeDownturn { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}