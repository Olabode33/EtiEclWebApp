using Abp.Application.Services.Dto;
using System;

namespace TestDemo.InvestmentInputs.Dtos
{
    public class GetAllInvestmentEclUploadsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string InvestmentEclReportingDateFilter { get; set; }

		 
    }
}