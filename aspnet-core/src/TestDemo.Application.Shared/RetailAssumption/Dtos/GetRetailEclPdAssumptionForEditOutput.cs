using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclPdAssumptionForEditOutput
    {
		public CreateOrEditRetailEclPdAssumptionDto RetailEclPdAssumption { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}