using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace TestDemo.InvestmentInputs.Dtos
{
    public class GetInvestmentAssetBookForEditOutput
    {
		public CreateOrEditInvestmentAssetBookDto InvestmentAssetBook { get; set; }

		public string InvestmentEclUploadUploadComment { get; set;}


    }
}