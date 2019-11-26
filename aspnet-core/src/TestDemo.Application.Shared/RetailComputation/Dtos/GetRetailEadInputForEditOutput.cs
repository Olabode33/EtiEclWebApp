using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailEadInputForEditOutput
    {
		public CreateOrEditRetailEadInputDto RetailEadInput { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}