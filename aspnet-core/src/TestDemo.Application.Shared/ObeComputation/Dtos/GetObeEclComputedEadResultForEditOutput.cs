using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.ObeComputation.Dtos
{
    public class GetObeEclComputedEadResultForEditOutput
    {
		public CreateOrEditObeEclComputedEadResultDto ObeEclComputedEadResult { get; set; }

		public string ObeEclDataLoanBookContractNo { get; set;}


    }
}