using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesaleEadInputAssumptionForEditOutput
    {
		public CreateOrEditWholesaleEadInputAssumptionDto WholesaleEadInputAssumption { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}