using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclPdAssumptionNonInteralModelForEditOutput
    {
		public CreateOrEditRetailEclPdAssumptionNonInteralModelDto RetailEclPdAssumptionNonInteralModel { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}