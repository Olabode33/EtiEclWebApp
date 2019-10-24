using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.WholesaleComputation.Dtos
{
    public class GetWholesaleEclComputedEadResultForEditOutput
    {
		public CreateOrEditWholesaleEclComputedEadResultDto WholesaleEclComputedEadResult { get; set; }

		public string WholesaleEclDataLoanBookCustomerName { get; set;}


    }
}