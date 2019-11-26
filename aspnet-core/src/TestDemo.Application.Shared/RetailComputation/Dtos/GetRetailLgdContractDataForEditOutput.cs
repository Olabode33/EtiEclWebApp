using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailLgdContractDataForEditOutput
    {
		public CreateOrEditRetailLgdContractDataDto RetailLgdContractData { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}