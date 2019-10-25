using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailAssumption.Dtos
{
    public class GetRetailEclPdSnPCummulativeDefaultRateForEditOutput
    {
		public CreateOrEditRetailEclPdSnPCummulativeDefaultRateDto RetailEclPdSnPCummulativeDefaultRate { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}