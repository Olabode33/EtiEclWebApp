using Abp.Application.Services.Dto;
using System;

namespace TestDemo.InvestmentInputs.Dtos
{
    public class GetAllInvestmentAssetBooksInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }


		 public string InvestmentEclUploadUploadCommentFilter { get; set; }

		 
    }
}