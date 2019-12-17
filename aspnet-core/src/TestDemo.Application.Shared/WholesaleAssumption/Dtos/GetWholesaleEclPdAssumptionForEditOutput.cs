using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesaleEclPdAssumptionForEditOutput
    {
		public CreateOrEditWholesaleEclPdAssumptionDto WholesaleEclPdAssumption { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}