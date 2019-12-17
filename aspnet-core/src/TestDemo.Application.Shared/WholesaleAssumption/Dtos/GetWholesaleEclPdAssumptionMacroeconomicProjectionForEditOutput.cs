using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesaleEclPdAssumptionMacroeconomicProjectionForEditOutput
    {
		public CreateOrEditWholesaleEclPdAssumptionMacroeconomicProjectionDto WholesaleEclPdAssumptionMacroeconomicProjection { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}