using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class GetInvestmentEclEadInputAssumptionForEditOutput
    {
		public CreateOrEditInvestmentEclEadInputAssumptionDto InvestmentEclEadInputAssumption { get; set; }

		public string InvestmentEclReportingDate { get; set;}


    }
}