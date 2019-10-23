using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesaleEclLgdAssumptionForEditOutput
    {
		public CreateOrEditWholesaleEclLgdAssumptionDto WholesaleEclLgdAssumption { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}