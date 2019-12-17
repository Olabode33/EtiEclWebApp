using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclPdAssumptionMacroeconomicInputForEditOutput
    {
		public CreateOrEditRetailEclPdAssumptionMacroeconomicInputDto RetailEclPdAssumptionMacroeconomicInput { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}