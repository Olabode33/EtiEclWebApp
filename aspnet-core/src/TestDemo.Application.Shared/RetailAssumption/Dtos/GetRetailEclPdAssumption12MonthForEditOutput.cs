using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclPdAssumption12MonthForEditOutput
    {
		public CreateOrEditRetailEclPdAssumption12MonthDto RetailEclPdAssumption12Month { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}