using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclEadInputAssumptionForEditOutput
    {
		public CreateOrEditRetailEclEadInputAssumptionDto RetailEclEadInputAssumption { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}