using Abp.Application.Services.Dto;
using System;

namespace TestDemo.InvestmentAssumption.Dtos
{
    public class GetAllInvestmentEclPdFitchDefaultRatesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string InvestmentEclReportingDateFilter { get; set; }

		 
    }
}