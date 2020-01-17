using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentComputation.Dtos
{
    public class GetInvestmentEclOverrideApprovalForEditOutput
    {
		public CreateOrEditInvestmentEclOverrideApprovalDto InvestmentEclOverrideApproval { get; set; }

		public string UserName { get; set;}

		public string InvestmentEclOverrideOverrideComment { get; set;}


    }
}