using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesaleEclAssumptionForEditOutput
    {
		public CreateOrEditWholesaleEclAssumptionDto WholesaleEclAssumption { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}