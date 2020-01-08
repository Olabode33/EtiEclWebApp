using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class GetInvestmentEclPdFitchDefaultRateForEditOutput
    {
		public CreateOrEditInvestmentEclPdFitchDefaultRateDto InvestmentEclPdFitchDefaultRate { get; set; }

		public string InvestmentEclReportingDate { get; set;}


    }
}