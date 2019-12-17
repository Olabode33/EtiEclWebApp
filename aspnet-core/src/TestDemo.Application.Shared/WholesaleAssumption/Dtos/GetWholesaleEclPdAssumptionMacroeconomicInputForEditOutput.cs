using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleAssumption.Dtos
{
    public class GetWholesaleEclPdAssumptionMacroeconomicInputForEditOutput
    {
		public CreateOrEditWholesaleEclPdAssumptionMacroeconomicInputDto WholesaleEclPdAssumptionMacroeconomicInput { get; set; }

		public string WholesaleEclTenantId { get; set;}


    }
}