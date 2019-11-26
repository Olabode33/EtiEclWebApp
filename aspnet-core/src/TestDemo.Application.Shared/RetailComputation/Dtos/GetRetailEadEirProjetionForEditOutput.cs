using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailEadEirProjetionForEditOutput
    {
		public CreateOrEditRetailEadEirProjetionDto RetailEadEirProjetion { get; set; }

		public string RetailEclTenantId { get; set;}


    }
}