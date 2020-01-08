using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class GetInvestmentEclPdInputAssumptionForEditOutput
    {
		public CreateOrEditInvestmentEclPdInputAssumptionDto InvestmentEclPdInputAssumption { get; set; }

		public string InvestmentEclReportingDate { get; set;}


    }
}