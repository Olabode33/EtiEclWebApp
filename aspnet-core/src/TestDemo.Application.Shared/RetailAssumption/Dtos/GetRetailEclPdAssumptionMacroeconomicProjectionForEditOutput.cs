using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclPdAssumptionMacroeconomicProjectionForEditOutput
    {
		public CreateOrEditRetailEclPdAssumptionMacroeconomicProjectionDto RetailEclPdAssumptionMacroeconomicProjection { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}