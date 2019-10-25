using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclLgdAssumptionForEditOutput
    {
		public CreateOrEditRetailEclLgdAssumptionDto RetailEclLgdAssumption { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}