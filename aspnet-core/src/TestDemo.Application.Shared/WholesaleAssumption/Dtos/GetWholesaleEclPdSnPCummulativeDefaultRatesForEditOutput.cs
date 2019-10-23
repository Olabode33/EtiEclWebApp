using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesaleEclPdSnPCummulativeDefaultRatesForEditOutput
    {
		public CreateOrEditWholesaleEclPdSnPCummulativeDefaultRatesDto WholesaleEclPdSnPCummulativeDefaultRates { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}