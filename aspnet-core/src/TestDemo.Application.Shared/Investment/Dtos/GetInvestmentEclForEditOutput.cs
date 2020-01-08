using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.Investment.Dtos
{
    public class GetInvestmentEclForEditOutput
    {
		public CreateOrEditInvestmentEclDto InvestmentEcl { get; set; }

		public string UserName { get; set;}


    }
}