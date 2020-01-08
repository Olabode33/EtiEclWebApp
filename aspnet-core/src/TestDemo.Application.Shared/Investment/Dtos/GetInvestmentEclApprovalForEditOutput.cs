using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.Investment.Dtos
{
    public class GetInvestmentEclApprovalForEditOutput
    {
		public CreateOrEditInvestmentEclApprovalDto InvestmentEclApproval { get; set; }

		public string UserName { get; set;}

		public string InvestmentEclReportingDate { get; set;}


    }
}