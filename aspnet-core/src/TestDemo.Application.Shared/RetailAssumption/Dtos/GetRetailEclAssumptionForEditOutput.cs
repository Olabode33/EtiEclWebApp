using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclAssumptionForEditOutput
    {
		public CreateOrEditRetailEclAssumptionDto RetailEclAssumption { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}