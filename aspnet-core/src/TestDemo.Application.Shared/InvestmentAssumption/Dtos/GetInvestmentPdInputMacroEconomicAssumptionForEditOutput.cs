using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class GetInvestmentPdInputMacroEconomicAssumptionForEditOutput
    {
		public CreateOrEditInvestmentPdInputMacroEconomicAssumptionDto InvestmentPdInputMacroEconomicAssumption { get; set; }

		public string InvestmentEclReportingDate { get; set;}


    }
}