using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclPdAssumptionNplIndexForEditOutput
    {
		public CreateOrEditRetailEclPdAssumptionNplIndexDto RetailEclPdAssumptionNplIndex { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}