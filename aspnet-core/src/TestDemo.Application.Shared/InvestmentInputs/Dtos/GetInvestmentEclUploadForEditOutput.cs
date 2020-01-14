using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentInputs.Dtos
{
    public class GetInvestmentEclUploadForEditOutput
    {
		public CreateOrEditInvestmentEclUploadDto InvestmentEclUpload { get; set; }

		public string InvestmentEclReportingDate { get; set;}


    }
}