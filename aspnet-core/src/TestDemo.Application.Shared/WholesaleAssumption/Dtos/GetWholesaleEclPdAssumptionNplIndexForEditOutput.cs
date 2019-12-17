using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesaleEclPdAssumptionNplIndexForEditOutput
    {
		public CreateOrEditWholesaleEclPdAssumptionNplIndexDto WholesaleEclPdAssumptionNplIndex { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}