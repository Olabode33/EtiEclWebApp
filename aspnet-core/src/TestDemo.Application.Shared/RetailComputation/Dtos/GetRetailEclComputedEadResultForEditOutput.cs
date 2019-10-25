using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.RetailComputation.Dtos
{
    public class GetRetailEclComputedEadResultForEditOutput
    {
		public CreateOrEditRetailEclComputedEadResultDto RetailEclComputedEadResult { get; set; }

		public string RetailEclDataLoanBookCustomerName { get; set;}


    }
}