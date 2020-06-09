using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class GetInvestmentEclOverrideForEditOutput
    {
		public CreateOrEditEclOverrideDto InvestmentEclOverride { get; set; }

		public string InvestmentEclSicrAssetDescription { get; set;}


    }
}